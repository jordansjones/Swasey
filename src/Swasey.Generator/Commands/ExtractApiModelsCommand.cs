using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Swasey.Lifecycle;
using Swasey.Normalization;

namespace Swasey.Commands
{
    internal class ExtractApiModelsCommand : ILifecycleCommand
    {

        public Task<ILifecycleContext> Execute(ILifecycleContext context)
        {
            var ctx = new GenerationContext(context)
            {
                State = LifecycleState.Continue
            };

            foreach (var modelTuple in ExtractModels(context))
            {
                ctx.NormalizationContext.Models.Add(ParseModelData(context, modelTuple));
            }

            return Task.FromResult<ILifecycleContext>(ctx);
        }

        private NormalizationApiModel ParseModelData(ILifecycleContext context, Tuple<string, dynamic> modelTuple)
        {
            var apiVersion = modelTuple.Item1;
            dynamic model = modelTuple.Item2;

            var normModel = new NormalizationApiModel
            {
                ApiNamespace = context.ApiNamespace,
                ApiVersion = apiVersion,
                ModelNamespace = context.ModelNamespace,
                Name = (string) model.id
            };

            if (model.ContainsKey("description") && !string.IsNullOrWhiteSpace((string) model.description))
            {
                normModel.Description = (string) model.description;
            }

            var requiredProperties = ParseRequiredProperties(model);

            foreach (NormalizationApiModelProperty prop in ParseProperties(model))
            {
                prop.ApiNamespace = context.ApiNamespace;
                prop.ApiVersion = apiVersion;
                prop.ModelNamespace = context.ModelNamespace;

                if (requiredProperties.Contains(prop.Name))
                {
                    prop.IsRequired = true;
                }
                normModel.Properties.Add(prop);
            }

            return normModel;
        }

        private IEnumerable<NormalizationApiModelProperty> ParseProperties(dynamic model)
        {
            if (!model.ContainsKey("properties")) goto NoMoreProperties;

            foreach (var propKv in model.properties)
            {
                var obj = propKv.Value;
                var prop = new NormalizationApiModelProperty
                {
                    Name = propKv.Key
                };

//                var prop = new ModelPropertyDefinition(context.ServiceMetadata)
//                {
//                    Name = propKv.Key,
//                    Type = DataType.ParseFromPropertyJObject(obj)
//                };
                if (obj.ContainsKey("description"))
                {
                    prop.Description = obj.description;
                }
                if (obj.ContainsKey("key"))
                {
                    prop.IsKey = (bool) obj.key;
                }

                yield return prop;
            }

        NoMoreProperties:
            yield break;
        }

        private HashSet<string> ParseRequiredProperties(dynamic model)
        {
            var requiredProperties = new HashSet<string>();

            if (model.ContainsKey("required"))
            {
                foreach (var n in model.required)
                {
                    var val = (string) n;
                    if (!string.IsNullOrWhiteSpace(val))
                    {
                        requiredProperties.Add(val);
                    }
                }
            }
            return requiredProperties;
        }

        private IEnumerable<Tuple<string, dynamic>> ExtractModels(ILifecycleContext context)
        {
            foreach (var apiKv in context.ApiPathJsonMapping)
            {
                var api = apiKv.Value;
                var apiVersion = (string) api.apiVersion;
                if (api.ContainsKey("models"))
                {
                    foreach (var modelKv in api.models)
                    {
                        if (modelKv != null && modelKv.Value != null)
                            yield return Tuple.Create(apiVersion, modelKv.Value);
                    }
                }
            }
        }

    }
}