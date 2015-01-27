using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Swasey.Lifecycle;
using Swasey.Model;

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
                ctx.RawModelDefinitions.Add(ParseModelData(context, modelTuple));
            }

            return Task.FromResult<ILifecycleContext>(ctx);
        }

        private IModelDefinition ParseModelData(ILifecycleContext context, Tuple<string, dynamic> modelTuple)
        {
            dynamic model = modelTuple.Item2;

            var modelDef = new ModelDefinition(context.ServiceMetadata)
            {
                ApiVersion = modelTuple.Item1,
                Name = (string) model.id,
                Type = "object"
            };

            if (model.ContainsKey("type"))
            {
                modelDef.Type = (string) model.type;
            }

            if (model.ContainsKey("description") && !string.IsNullOrWhiteSpace((string) model.description))
            {
                modelDef.Description = (string) model.description;
            }

            modelDef.Properties.AddRange(ParseProperties(context, model, ParseRequiredProperties(model)));

            return modelDef;
        }

        private IEnumerable<ModelPropertyDefinition> ParseProperties(ILifecycleContext context, dynamic model, HashSet<string> requiredProperties)
        {
            if (!model.ContainsKey("properties")) goto NoMoreProperties;

            foreach (var propKv in model.properties)
            {
                var prop = new ModelPropertyDefinition(context.ServiceMetadata)
                {
                    Name = propKv.Key
                };
                var db = true;
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