using System;
using System.Linq;
using System.Threading.Tasks;

using Swasey.Lifecycle;

namespace Swasey.Commands
{
    public class ValidateSwaggerApiDefinitionCommand : ILifecycleCommand
    {

        public Task<ILifecycleContext> Execute(ILifecycleContext context)
        {
            foreach (var kv in context.ApiPathJsonMapping)
            {
                if (kv.Value == null)
                    throw new SwaseyException("No JSON parsed for api '{0}'", kv.Key);

                var json = kv.Value;

                if (!json.ContainsKey("apiVersion") || string.IsNullOrWhiteSpace((string) json.apiVersion))
                    throw new SwaseyException("apiVersion is required for api: '{0}'", kv.Key);

                if (!json.ContainsKey("swaggerVersion") || string.IsNullOrWhiteSpace((string) json.swaggerVersion))
                    throw new SwaseyException("swaggerVersion is required for api: '{0}'", kv.Key);

                if (!json.ContainsKey("basePath") || string.IsNullOrWhiteSpace((string) json.basePath))
                    throw new SwaseyException("basePath is required for api: '{0}'", kv.Key);

                if (!json.ContainsKey("resourcePath") || string.IsNullOrWhiteSpace((string) json.resourcePath))
                    throw new SwaseyException("resourcePath is required for api: '{0}'", kv.Key);

                if (json.ContainsKey("models") && json.models.Length > 0)
                {
                    foreach (var modelKv in json.models)
                    {
                        if (modelKv.Key == null) continue;
                        if (modelKv.Value == null)
                            throw new SwaseyException("listed model has no model definition [{0}]: '{1}'", modelKv.Key, modelKv.Value);

                        if (!modelKv.Value.ContainsKey("id") || string.IsNullOrWhiteSpace((string) modelKv.Value.id))
                            throw new SwaseyException("id is required for api model: '{0}'", modelKv.Key);
                    }
                }
            }

            return Task.FromResult(context);
        }

    }
}
