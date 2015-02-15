using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Swasey.Model
{
    [DebuggerDisplay("{DebuggerDisplay, nq}", Name = "{Name}")]
    internal class ModelDefinition : BaseModelEntityDefinition, IModelDefinition
    {

        private readonly List<ModelPropertyDefinition> _properties = new List<ModelPropertyDefinition>();

        public ModelDefinition(IServiceMetadata meta) : base(meta) {}

        public ModelDefinition(IModelDefinition copyFrom) : base(copyFrom)
        {
            if (copyFrom != null && copyFrom.Properties.Any())
            {
                Properties.AddRange(copyFrom.Properties.Select(x => new ModelPropertyDefinition(x)));
            }
        }

        public string ContextName { get; set; }

        private string DebuggerDisplay
        {
            get { return string.Join(", ", Properties.Select(x => string.Format("{{{0}: {1}}}", x.Name, x.Type))); }
        }

        public List<ModelPropertyDefinition> Properties
        {
            get { return _properties; }
        }

        public IReadOnlyList<IModelPropertyDefinition> DefaultValueProperties
        {
            get { return Properties.Where(x => x.Type.HasDefaultValue).ToList(); }
        }

        public bool HasDefaultValueProperties
        {
            get { return Properties.Any(x => x.Type.HasDefaultValue); }
        }

        public bool HasKeyProperty
        {
            get { return Properties.Any(x => x.IsKey); }
        }

        public bool HasMaximumValueProperties
        {
            get { return Properties.Any(x => x.Type.HasMaximumValue); }
        }

        public bool HasMinimumValueProperties
        {
            get { return Properties.Any(x => x.Type.HasMinimumValue); }
        }

        public bool HasRequiredProperties
        {
            get { return Properties.Any(x => x.IsRequired); }
        }

        public IModelPropertyDefinition KeyProperty
        {
            get { return Properties.FirstOrDefault(x => x.IsKey); }
        }

        public IReadOnlyList<IModelPropertyDefinition> MaximumValueProperties
        {
            get { return Properties.Where(x => x.Type.HasMaximumValue).ToList(); }
        }

        public IReadOnlyList<IModelPropertyDefinition> MinimumValueProperties
        {
            get { return Properties.Where(x => x.Type.HasMinimumValue).ToList(); }
        }

        IReadOnlyList<IModelPropertyDefinition> IModelDefinition.Properties
        {
            get { return Properties; }
        }

        public IReadOnlyList<IModelPropertyDefinition> RequiredProperties
        {
            get { return Properties.Where(x => x.IsRequired).ToList(); }
        }

    }
}
