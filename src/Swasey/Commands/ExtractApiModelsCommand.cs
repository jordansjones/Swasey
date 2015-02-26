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
            var ctx = new LifecycleContext(context)
            {
                State = LifecycleState.Continue
            };

            foreach (var modelObj in ExtractModels(context))
            {
                var modelType = (string) modelObj.Model.type;
                var isEnum = "enum".Equals(modelType, StringComparison.InvariantCultureIgnoreCase);

                if (isEnum)
                {
                    var model = ParseEnumData(modelObj);
                    model.ApiNamespace = context.ApiNamespace;
                    model.ModelNamespace = context.ModelNamespace;
                    ctx.NormalizationContext.Enums.Add(model);
                }
                else
                {
                    var model = ParseModelData(modelObj);
                    model.ApiNamespace = context.ApiNamespace;
                    model.ModelNamespace = context.ModelNamespace;

                    foreach (var prop in model.Properties)
                    {
                        prop.ApiNamespace = context.ApiNamespace;
                        prop.ModelNamespace = context.ModelNamespace;
                    }

                    ctx.NormalizationContext.Models.Add(model);
                }
            }

            foreach (var model in ctx.NormalizationContext.Models.Where(x => x.RawSubTypes.Any()))
            {
                foreach (var st in model.RawSubTypes)
                {
                    var sm = ctx.NormalizationContext.Models.FirstOrDefault(x => x.Name.Equals(st, StringComparison.InvariantCultureIgnoreCase));
                    if (sm == null) continue;
                    model.SubTypes.Add(sm);
                }
            }

            var enumNames = ctx.NormalizationContext.Enums.Select(x => x.Name).ToList();
            var modelNames = ctx.NormalizationContext.Models.Select(x => x.Name).ToList();

            // Ensure that Enum Properties are properly indicated
            ctx.NormalizationContext.Models
                .SelectMany(x => x.Properties)
                .Where(x => enumNames.Contains(x.TypeName) && !modelNames.Contains(x.TypeName))
                .ToList()
                .ForEach(x => x.IsEnum = true);

            return Task.FromResult<ILifecycleContext>(ctx);
        }

        private NormalizationApiModelEnum ParseEnumData(dynamic item)
        {
            var apiVersion = (string) item.ApiVersion;
            var resourceName = (string) item.ResourceName;
            var resourcePath = (string) item.ResourcePath;
            dynamic model = item.Model;

            var normEnum = new NormalizationApiModelEnum
            {
                ApiVersion = apiVersion,
                Name = (string) model.id,
                ResourceName = resourceName,
                ResourcePath = resourcePath
            };

            if (model.ContainsKey("description") && !string.IsNullOrWhiteSpace((string) model.description))
            {
                normEnum.Description = (string) model.description;
            }

            var type = new SimpleNormalizationApiDataType(model);
            type.TryParseEnumFromJObject(model);

            var values = type.EnumValues;
            if (values.Any()) normEnum.Values.AddRange(values);

            return normEnum;
        }

        private NormalizationApiModel ParseModelData(dynamic item)
        {
            var apiVersion = (string) item.ApiVersion;
            var resourceName = (string) item.ResourceName;
            var resourcePath = (string) item.ResourcePath;
            dynamic model = item.Model;

            var normModel = new NormalizationApiModel
            {
                ApiVersion = apiVersion,
                Name = (string) model.id,
                ResourceName = resourceName,
                ResourcePath = resourcePath
            };

            if (model.ContainsKey("subTypes"))
            {
                foreach (var sto in model.subTypes)
                {
                    if (sto == null) continue;
                    var st = (string) sto;
                    if (string.IsNullOrWhiteSpace(st)) continue;

                    normModel.RawSubTypes.Add(st);
                }
            }

            if (model.ContainsKey("description") && !string.IsNullOrWhiteSpace((string) model.description))
            {
                normModel.Description = (string) model.description;
            }

            var requiredProperties = ParseRequiredProperties(model);

            foreach (NormalizationApiModelProperty prop in ParseProperties(model))
            {
                prop.ApiVersion = apiVersion;

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

                prop.CopyFrom(SimpleNormalizationApiDataType.ParseFromJObject(obj));
                
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

        private IEnumerable<dynamic> ExtractModels(ILifecycleContext context)
        {
            foreach (var apiKv in context.ApiPathJsonMapping)
            {
                var api = apiKv.Value;
                var apiVersion = (string) api.apiVersion;
                var resourcePath = (string) api.resourcePath;
                var resourceName = resourcePath.ResourceNameFromPath();
                if (api.ContainsKey("models"))
                {
                    foreach (var modelKv in api.models)
                    {
                        if (modelKv != null && modelKv.Value != null)
                            yield return new
                            {
                                ApiVersion = apiVersion,
                                Model = modelKv.Value,
                                ResourceName = resourceName,
                                ResourcePath = resourcePath
                            };
                    }
                }
            }
        }

    }
}