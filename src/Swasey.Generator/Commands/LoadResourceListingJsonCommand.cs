using System;
using System.Linq;
using System.Threading.Tasks;

using Jil;

using Swasey.Lifecycle;

namespace Swasey.Commands
{
    internal class LoadResourceListingJsonCommand : ILifecycleCommand
    {

        public async Task<ILifecycleContext> Execute(ILifecycleContext context)
        {
            var json = await context.Loader(context.ResourceListingUri);
            if (string.IsNullOrWhiteSpace(context.ResourceListingJson))
                throw new SwaseyException("Invalid Resource Listing JSON: '{0}'", context.ResourceListingJson);

            var rl = JSON.DeserializeDynamic(json);
            if (rl == null)
                throw new SwaseyException("Unable to parse Resource Listing JSON: '{0}'", context.ResourceListingJson);

            return new GenerationContext(context)
            {
                State = LifecycleState.Continue,
                ResourceListingJson = rl
            };
        }

    }
}
