using System;
using System.Linq;
using System.Threading.Tasks;

using Jil;

using Swasey.Lifecycle;

namespace Swasey.Commands
{
    internal class LoadApiJsonCommand : ILifecycleCommand
    {

        public async Task<ILifecycleContext> Execute(ILifecycleContext context)
        {
            var ctx = new GenerationContext(context)
            {
                State = LifecycleState.Continue
            };

            var basePath = context.ResourceListingUri;

            foreach (var path in ctx.ApiPathJsonMapping.Keys.ToList())
            {
                var json = await context.Loader(new Uri(basePath, path));
                if (string.IsNullOrWhiteSpace(json))
                    throw new SwaseyException("Invalid JSON for api [{0}]: '{1}'", path, json);

                var obj = JSON.DeserializeDynamic(json);
                if (obj == null)
                    throw new SwaseyException("Unable to parse api definition JSON [{0}]: '{1}'", path, json);

                ctx.ApiPathJsonMapping[path] = obj;
            }

            return ctx;
        }

    }
}
