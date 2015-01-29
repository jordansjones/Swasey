using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Swasey.Lifecycle;

namespace Swasey.Commands
{
    internal class ExtractApiOperationsCommand : ILifecycleCommand
    {

        public Task<ILifecycleContext> Execute(ILifecycleContext context)
        {
            var ctx = new GenerationContext(context)
            {
                State = LifecycleState.Continue
            };

            foreach (var opTuple in ExtractOperations(context))
            {
                
            }

            return Task.FromResult<ILifecycleContext>(ctx);
        }

        private IEnumerable<Tuple<string, dynamic>> ExtractOperations(ILifecycleContext context)
        {
            foreach (var apiKv in context.ApiPathJsonMapping)
            {
//                foreach ()
            }
            yield break;
        }

    }
}
