using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Swasey.Lifecycle;
using Swasey.Normalization;

namespace Swasey.Commands
{
    internal class ExtractApiOperationsCommand : ILifecycleCommand
    {

        public Task<ILifecycleContext> Execute(ILifecycleContext context)
        {
            var ctx = new LifecycleContext(context)
            {
                State = LifecycleState.Continue
            };

            foreach (var apiOp in ExtractApiOperations(context))
            {
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

        private NormalizationApiOperationResponse ParseResponse(dynamic op)
        {
            var dataType = SimpleNormalizationApiDataType.ParseFromJObject(op);
            var resp = new NormalizationApiOperationResponse();
            resp.CopyFrom(dataType);

            return resp;
        }

        private IEnumerable<NormalizationApiOperationParameter> ParseParameters(dynamic op)
        {
            if (!op.ContainsKey("parameters")) goto NoMoreParameters;

            foreach (var paramObj in op.parameters)
            {
                var param = new NormalizationApiOperationParameter
                {
                    AllowsMultiple = paramObj.ContainsKey("allowMultiple") && (bool) paramObj.allowMultiple,
                    Description = paramObj.ContainsKey("description") ? (string) paramObj.description : string.Empty,
                    Name = paramObj.ContainsKey("name") ? (string) paramObj.name : string.Empty
                };

                param.CopyFrom(SimpleNormalizationApiDataType.ParseFromJObject(paramObj));

                yield return param;
            }

        NoMoreParameters:
            yield break;
        }

        private IEnumerable<dynamic> ExtractApiOperations(ILifecycleContext context)
        {
            foreach (var apiKv in context.ApiPathJsonMapping)
            {
                var apiDef = apiKv.Value;
                var apiVersion = (string) apiDef.apiVersion;
                var basePath = (string) apiDef.basePath;
                var resourcePath = (string) apiDef.resourcePath;
                if (!apiDef.ContainsKey("apis")) continue;

                foreach (var api in apiDef.apis)
                {
                    if (api == null || !api.ContainsKey("path") || !api.ContainsKey("operations")) continue;
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
            yield break;
        }

    }
}
