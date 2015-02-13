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
            var enumWriter = context.EnumWriter;
            var modelWriter = context.ModelWriter;

            var sb = new StringBuilder();

            // Enums First
            var template = context.ApiEnumTemplate;
            foreach (var obj in svcDef.Enums.SelectMany(x => x))
            {
                using (var sw = new StringWriter(sb.Clear()))
                {
                    template(sw, obj);
                }

                enumWriter(obj.Name, sb.ToString(), obj);
            }

            // Models Second
            template = context.ApiModelTemplate;
            foreach (var obj in svcDef.Models.SelectMany(x => x))
            {
                using (var sw = new StringWriter(sb.Clear()))
                {
                    template(sw, obj);
                }

                modelWriter(obj.Name, sb.ToString(), obj);
            }

            return Task.FromResult(context);
        }

    }
}
