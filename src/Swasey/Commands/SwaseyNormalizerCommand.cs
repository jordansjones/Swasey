using System;
using System.Linq;
using System.Threading.Tasks;

using Swasey.Lifecycle;

namespace Swasey.Commands
{
    internal class SwaseyNormalizerCommand : ILifecycleCommand
    {

        public Task<ILifecycleContext> Execute(ILifecycleContext context)
        {
            var serviceDefinition = context.Normalizer(context.NormalizationContext);

            var ctx = new GenerationContext(context)
            {
                State = LifecycleState.Continue,
                ServiceDefinition = serviceDefinition
            };

            return Task.FromResult<ILifecycleContext>(ctx);
        }

    }
}
