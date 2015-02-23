using System;
using System.Linq;
using System.Threading.Tasks;

using Swasey.Lifecycle;
using Swasey.Model;
using Swasey.Normalization;

namespace Swasey.Commands
{
    internal class NormalizeEnumsCommand : ILifecycleCommand
    {

        public Task<ILifecycleContext> Execute(ILifecycleContext context)
        {
            var serviceDefinition = new ServiceDefinition(context.ServiceDefinition);

            var enums = context.NormalizationContext
                .Enums
                .Select(CreateDefinition)
                .ToLookup(x => x.Name);

            if (!enums.Any())
            {
                goto ReturnResult;
            }

            foreach (var e in enums)
            {
                var x = e.FirstOrDefault();
                if (x != null)
                {
                    serviceDefinition.AddEnum(x);
                }
            }


            ReturnResult:
            var ctx = new LifecycleContext(context)
            {
                ServiceDefinition = serviceDefinition,
                State = LifecycleState.Continue
            };
            return Task.FromResult<ILifecycleContext>(ctx);
        }

        private EnumDefinition CreateDefinition(NormalizationApiModelEnum normalEnum)
        {
            var ed = new EnumDefinition(normalEnum.AsMetadata())
            {
                ContextName = "Enums",
                Description = normalEnum.Description,
                Name = normalEnum.Name.MapDataTypeName(),
                ResourceName = "Enums"
            };

            for (var i = 0; i < normalEnum.Values.Count; i++)
            {
                var item = normalEnum.Values[i];
                var idx = i;

                ed.Values.Add(item, idx);
            }
            return ed;
        }

    }
}
