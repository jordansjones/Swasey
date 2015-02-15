using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Swasey.Model
{
    [DebuggerDisplay("{DebuggerDisplay, nq}", Name = "[ENUM] {Name}")]
    internal class EnumDefinition : BaseModelEntityDefinition, IEnumDefinition, IEquatable<EnumDefinition>
    {

        private readonly Dictionary<string, int> _values = new Dictionary<string, int>();

        public EnumDefinition(IServiceMetadata meta) : base(meta) {}

        public EnumDefinition(IEnumDefinition copyFrom)
            : base(copyFrom)
        {
            if (copyFrom != null && copyFrom.Values.Any())
            {
                foreach (var kv in copyFrom.Values)
                {
                    Values.Add(kv.Key, kv.Value);
                }
            }
        }

        public string ContextName { get; set; }

        public Dictionary<string, int> Values
        {
            get { return _values; }
        }

        IReadOnlyList<KeyValuePair<string, int>> IEnumDefinition.Values
        {
            get { return Values.ToList(); }
        }

        private string DebuggerDisplay
        {
            get { return string.Join(", ", Values.Select(x => string.Format("{{{0}: {1}}}", x.Value, x.Key))); }
        }

        private bool CompareValues(Dictionary<string, int> others)
        {
            if (Values.Count != others.Count) return false;

            foreach (var kv in others)
            {
                int v;
                if (!Values.TryGetValue(kv.Key, out v)) return false;
                if (v != kv.Value) return false;
            }
            return true;
        }

        public bool Equals(EnumDefinition other)
        {
            if (ReferenceEquals(null, other)) { return false; }
            if (ReferenceEquals(this, other)) { return true; }
            return CompareValues(other._values) && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) { return false; }
            if (ReferenceEquals(this, obj)) { return true; }
            if (obj.GetType() != this.GetType()) { return false; }
            return Equals((EnumDefinition) obj);
        }

        public override int GetHashCode()
        {
            unchecked { return ((_values != null ? _values.GetHashCode() : 0) * 397) ^ (ContextName != null ? ContextName.GetHashCode() : 0); }
        }

        public static bool operator ==(EnumDefinition left, EnumDefinition right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EnumDefinition left, EnumDefinition right)
        {
            return !Equals(left, right);
        }

    }
}
