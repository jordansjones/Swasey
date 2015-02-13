using System;
using System.Linq;
using System.Threading.Tasks;

namespace Swasey.Lifecycle
{
    internal interface ILifecycle
    {

        ILifecycle Enqueue(ILifecycleCommand command);

        Task<ILifecycleContext> Execute(ILifecycleContext context);

    }
}
