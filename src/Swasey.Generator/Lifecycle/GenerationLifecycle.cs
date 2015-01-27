using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swasey.Lifecycle
{
    internal class GenerationLifecycle : ILifecycle
    {

        private readonly Queue<ILifecycleCommand> _commands = new Queue<ILifecycleCommand>();


        public ILifecycle Enqueue(ILifecycleCommand command)
        {
            _commands.Enqueue(command);
            return this;
        }

        public async Task<ILifecycleContext> Execute(ILifecycleContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            while (_commands.Count > 0 && context.State == LifecycleState.Continue)
            {
                var cmd = _commands.Dequeue();

                context = await cmd.Execute(context);
            }

            return context;
        }

    }
}
