using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Swasey.Lifecycle;
using Swasey.Model;
using Swasey.Normalization;

namespace Swasey.Commands
{
    internal class NormalizeEnumsCommand : ILifecycleCommand
    {

        private readonly Dictionary<string, int> _enumNames = new Dictionary<string, int>();

        public Task<ILifecycleContext> Execute(ILifecycleContext context)
        {

            var serviceDefinition = new ServiceDefinition(context.ServiceDefinition);

            var enums = context.NormalizationContext
                .Models
                .SelectMany(x => x)
                .SelectMany(ExtractModelEnums)
                .Concat(
                    context.NormalizationContext
                        .Operations
                        .SelectMany(x => x)
                        .SelectMany(ExtractOperationEnums)
                )
                .ToList();

            if (!enums.Any())
            {
                goto ReturnResult;
            }
            enums.ForEach(x => serviceDefinition.AddEnum(x));
            

        ReturnResult:
            var ctx = new LifecycleContext(context)
            {
                ServiceDefinition = serviceDefinition,
                State = LifecycleState.Continue
            };
            return Task.FromResult<ILifecycleContext>(ctx);
        }

        private IEnumerable<EnumDefinition> ExtractModelEnums(INormalizationApiModel model)
        {
            foreach (var x in model.Properties.Values.Where(x => x.IsEnum))
            {
                var ed = CreateDefinition(x, model.Name, x.Name, x.Description, x.EnumValues, x.TypeName);
                x.SetTypeName(ed.Name);
                yield return ed;
            }
        }

        private IEnumerable<EnumDefinition> ExtractOperationEnums(INormalizationApiOperation operation)
        {
            foreach (var x in operation.Parameters.Where(x => x.IsEnum))
            {
                var ed = CreateDefinition(x, operation.Name, x.Name, x.Description, x.EnumValues, x.TypeName);
                x.SetTypeName(ed.Name);
                yield return ed;
            }
        }

        private EnumDefinition CreateDefinition(INormalizationEntity meta, string contextName, string name, string description, string[] values, string typeName)
        {
            var ed = new EnumDefinition(meta.AsMetadata())
            {
                ContextName = contextName,
                Description = description,
                Name = name
            };

            ed.Name = NormalizeEnumName(ed, name);

            // Normalize TypeName
            typeName = typeName.Trim().ToUpperInvariant();

            for (var i = 0; i < values.Length; i++)
            {
                var item = values[i];
                var idx = i;
                switch (typeName)
                {
                    case "STRING":
                        break;
                    case "INT":
                        var offset = item.IndexOf('-');
                        if (offset > 0)
                        {
                            idx = int.Parse(item.Substring(0, offset));
                            item = item.Substring(offset + 1);
                        }
                        break;
                    default:
                        throw new NotImplementedException(string.Format("'{0}' isn't handled", typeName));
                }
                
                ed.Values.Add(Kv(item, idx));
            }
            return ed;
        }

        private string NormalizeEnumName(EnumDefinition ed, string ename)
        {
            var count = 0;
            if (!_enumNames.TryGetValue(ename, out count))
            {
                _enumNames.Add(ename, ++count);
                return ename;
            }

            var isFirstAttempt = !ename.StartsWith(ed.ContextName);
            ename = ed.ContextName + ed.Name;
            if (!isFirstAttempt)
            {
                ename += count;
            }
            return NormalizeEnumName(ed, ename);
        }

        private KeyValuePair<string, int> Kv(string key, int val)
        {
            return new KeyValuePair<string, int>(key, val);
        }

    }
}
