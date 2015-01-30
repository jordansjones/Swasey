using System;
using System.Linq;
using System.Threading.Tasks;

namespace Swasey.Lifecycle
{
    public interface ILifecycleCommand
    {

        Task<ILifecycleContext> Execute(ILifecycleContext context);

    }
}
