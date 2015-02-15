using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Swasey.Lifecycle;
using Swasey.Model;
using Swasey.Normalization;

namespace Swasey.Commands
{
    internal class ExtractApiOperationsCommand : ILifecycleCommand
    {

        public Func<dynamic, bool> OperationFilter { get; private set; }

        public Func<dynamic, bool> OperationParameterFilter { get; private set; }

        public Task<ILifecycleContext> Execute(ILifecycleContext context)
        {
            OperationFilter = context.OperationFilter ?? Defaults.DefaultOperationFilter;
            OperationParameterFilter = context.OperationParameterFilter ?? Defaults.DefaultOperationParameterFilter;

            var ctx = new LifecycleContext(context)
            {
                State = LifecycleState.Continue
            };

            foreach (var apiOp in ExtractApiOperations(context))
            {
                if (!OperationFilter(apiOp.JObject)) continue;

                NormalizationApiOperation op = ParseOperationData(apiOp);
                op.ApiNamespace = context.ApiNamespace;
                op.ModelNamespace = context.ModelNamespace;

                op.Response.ApiNamespace = context.ApiNamespace;
                op.Response.ModelNamespace = context.ModelNamespace;

                foreach (var param in op.Parameters)
                {
                    param.ApiNamespace = context.ApiNamespace;
                    param.ModelNamespace = context.ModelNamespace;
                }

                ctx.NormalizationContext.Operations.Add(op);
            }

            return Task.FromResult<ILifecycleContext>(ctx);
        }

        private IEnumerable<dynamic> ExtractApiOperations(ILifecycleContext context)
        {
            foreach (var apiKv in context.ApiPathJsonMapping)
            {
                var apiDef = apiKv.Value;
                var apiVersion = (string) apiDef.apiVersion;
                var basePath = (string) apiDef.basePath;
                var resourcePath = (string) apiDef.resourcePath;
                if (!apiDef.ContainsKey("apis")) { continue; }

                foreach (var api in apiDef.apis)
                {
                    if (api == null || !api.ContainsKey("path") || !api.ContainsKey("operations")) { continue; }
                    var opPath = (string) api.path;
                    foreach (var op in api.operations)
                    {
                        yield return new
                        {
                            ApiVersion = apiVersion,
                            BasePath = basePath,
                            OperationPath = opPath,
                            ResourcePath = resourcePath,
                            JObject = op
                        };
                    }
                }
            }
        }

        private NormalizationApiOperation ParseOperationData(dynamic extractedOp)
        {
            var opObj = extractedOp.JObject;

            var op = new NormalizationApiOperation
            {
                ApiVersion = (string) extractedOp.ApiVersion,
                BasePath = (string) extractedOp.BasePath,
                Path = (string) extractedOp.OperationPath,
                ResourcePath = (string) extractedOp.ResourcePath,
                HttpMethod = (opObj.ContainsKey("method") ? (string) opObj.method : string.Empty).ParseHttpMethodType(),
                Description = opObj.ContainsKey("summary") ? (string) opObj.summary : string.Empty,
                Name = opObj.ContainsKey("nickname") ? (string) opObj.nickname : string.Empty
            };

            op.Parameters.AddRange(ParseParameters(opObj));
            op.Response = ParseResponse(opObj);

            return op;
        }

        private IEnumerable<NormalizationApiOperationParameter> ParseParameters(dynamic op)
        {
            if (!op.ContainsKey("parameters")) { goto NoMoreParameters; }

            foreach (var paramObj in op.parameters)
            {
                if (!OperationParameterFilter(paramObj)) continue;
                if (!paramObj.ContainsKey("paramType")) continue;

                var param = new NormalizationApiOperationParameter();
                param.CopyFrom(SimpleNormalizationApiDataType.ParseFromJObject(paramObj));

                param.AllowsMultiple = paramObj.ContainsKey("allowMultiple") && (bool) paramObj.allowMultiple;
                param.Description = paramObj.ContainsKey("description") ? (string) paramObj.description : string.Empty;
                param.Name = paramObj.ContainsKey("name") ? (string) paramObj.name : string.Empty;
                param.ParameterType = GetParamType(paramObj);
                param.IsRequired = paramObj.ContainsKey("required") && (bool) paramObj.required;

                yield return param;
            }

            NoMoreParameters:
            ;
        }

        private NormalizationApiOperationResponse ParseResponse(dynamic op)
        {
            var dataType = SimpleNormalizationApiDataType.ParseFromJObject(op);
            var resp = new NormalizationApiOperationResponse();
            resp.CopyFrom(dataType);

            return resp;
        }

        private ParameterType GetParamType(dynamic op)
        {
            var p = ((string) op.paramType).Trim().ToLowerInvariant();
            return p.ParseParameterType();
        }

    }
}
