using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Swasey.Model
{
    [DebuggerDisplay("{DebuggerDisplay, nq}", Name = "[ENUM] {Name}")]
    internal class EnumDefinition : BaseModelEntityDefinition, IEnumDefinition
    {

        private readonly List<KeyValuePair<string, int>> _values = new List<KeyValuePair<string, int>>();

        public EnumDefinition(IServiceMetadata meta) : base(meta) {}

        public EnumDefinition(IEnumDefinition copyFrom)
            : base(copyFrom)
        {
            if (copyFrom != null && copyFrom.Values.Any())
            {
                Values.AddRange(copyFrom.Values);
            }
        }

        public string ContextName { get; set; }

        public List<KeyValuePair<string, int>> Values
        {
            get { return _values; }
        }

        IReadOnlyList<KeyValuePair<string, int>> IEnumDefinition.Values
        {
            get { return Values; }
        }

        private string DebuggerDisplay
        {
            get { return string.Join(", ", Values.Select(x => string.Format("{{{0}: {1}}}", x.Value, x.Key))); }
        }
    }
}
