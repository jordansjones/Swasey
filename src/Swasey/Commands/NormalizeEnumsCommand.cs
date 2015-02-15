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
                .SelectMany(ExtractModelEnums)
                .Concat(
                    context.NormalizationContext
                        .Operations
                        .SelectMany(ExtractOperationEnums)
                )
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

        private EnumDefinition CreateDefinition(BaseNormalizationEntity meta, string contextName, string name, string description, string[] values, string typeName, string resourcePath)
        {
            var ed = new EnumDefinition(meta.AsMetadata())
            {
                ContextName = contextName,
                Description = description,
                Name = name,
                ResourceName = resourcePath.ResourceNameFromPath()
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

                ed.Values.Add(item, idx);
            }
            return ed;
        }

        private IEnumerable<EnumDefinition> ExtractModelEnums(NormalizationApiModel model)
        {
            foreach (var x in model.Properties.Where(x => x.IsEnum))
            {
                var ed = CreateDefinition(x, model.Name, x.Name, x.Description, x.EnumValues, x.TypeName, model.ResourcePath);
                x.SetTypeName(ed.Name);
                yield return ed;
            }
        }

        private IEnumerable<EnumDefinition> ExtractOperationEnums(NormalizationApiOperation operation)
        {
            foreach (var x in operation.Parameters.Where(x => x.IsEnum))
            {
                var ed = CreateDefinition(x, operation.Name, x.Name, x.Description, x.EnumValues, x.TypeName, operation.ResourcePath);
                x.SetTypeName(ed.Name);
                yield return ed;
            }
        }

        private KeyValuePair<string, int> Kv(string key, int val)
        {
            return new KeyValuePair<string, int>(key, val);
        }

        private string NormalizeEnumName(EnumDefinition ed, string ename, int count = 0)
        {
            int refCount = 0;
            if (!_enumNames.TryGetValue(ename, out refCount))
            {
                _enumNames.Add(ename, ++count);
                return ename;
            }

            var isFirstAttempt = !ename.StartsWith(ed.ContextName);
            ename = ed.ContextName + ed.Name;
            if (!isFirstAttempt)
            {
                return ename;
            }
            return NormalizeEnumName(ed, ename, count);
        }

    }
}
