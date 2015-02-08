using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Swasey.Lifecycle;

namespace Swasey.Commands
{
    internal class GenerateApiSourcesCommand : ILifecycleCommand
    {

        public Task<ILifecycleContext> Execute(ILifecycleContext context)
        {
            var svcDef = context.ServiceDefinition;
            var template = context.ApiOperationTemplate;
            var swaseyWriter = context.Writer;

            var sb = new StringBuilder();

            foreach (var op in svcDef.ResourceOperations.SelectMany(x => x))
            {
                using (var sw = new StringWriter(sb.Clear()))
                {
                    template(sw, op);
                }
                swaseyWriter(WriteType.Operation, op.Name, sb.ToString());
            }

            return Task.FromResult(context);
        }

    }
}
