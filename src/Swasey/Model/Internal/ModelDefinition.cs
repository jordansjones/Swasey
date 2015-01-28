using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    internal class ModelDefinition : BaseDefinition, IModelDefinition
    {

        public ModelDefinition(IServiceMetadata meta) : base(meta)
        {
            Properties = new List<IModelPropertyDefinition>();
        }

        public List<IModelPropertyDefinition> Properties { get; private set; }

        public QualifiedName Name { get; set; }

        IReadOnlyList<IModelPropertyDefinition> IModelDefinition.Properties
        {
            get { return Properties; }
        }

        public bool HasDescription
        {
            get { return !string.IsNullOrWhiteSpace(Description); }
        }

        public string Description { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0}", Name);
        }

    }
}
