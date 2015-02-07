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

        public List<ModelPropertyDefinition> Properties
        {
            get { return _properties; }
        }

        IReadOnlyList<IModelPropertyDefinition> IModelDefinition.Properties
        {
            get { return Properties; }
        }

        private string DebuggerDisplay
        {
            get { return string.Join(", ", Properties.Select(x => string.Format("{{{0}: {1}}}", x.Name, x.Type))); }
        }

    }
}
