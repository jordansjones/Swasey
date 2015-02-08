using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Swasey.Lifecycle;

namespace Swasey.Commands
{
    internal class GenerateModelSourcesCommand : ILifecycleCommand
    {

        public Task<ILifecycleContext> Execute(ILifecycleContext context)
        {
            var svcDef = context.ServiceDefinition;
            var swaseyWriter = context.Writer;

            var sb = new StringBuilder();

            // Enums First
            var template = context.ApiEnumTemplate;
            foreach (var obj in svcDef.Enums.SelectMany(x => x))
            {
                using (var sw = new StringWriter(sb.Clear()))
                {
                    template(sw, obj);
                }

                swaseyWriter(WriteType.Enum, obj.Name, sb.ToString());
            }

            // Models Second
            template = context.ApiModelTemplate;
            foreach (var obj in svcDef.Models.SelectMany(x => x))
            {
                using (var sw = new StringWriter(sb.Clear()))
                {
                    template(sw, obj);
                }

                swaseyWriter(WriteType.Model, obj.Name, sb.ToString());
            }

            return Task.FromResult(context);
        }

    }
}
