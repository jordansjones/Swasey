using System;
using System.Linq;

namespace Swasey.Model
{
    internal abstract class BaseModelEntityDefinition : BaseDefinition
    {

        protected BaseModelEntityDefinition(IServiceMetadata meta) : base(meta) {}

        protected BaseModelEntityDefinition(IModelEntity copyFrom) : base(copyFrom)
        {
            Description = copyFrom.Description;
            Name = copyFrom.Name;
        }

        public string Description { get; set; }

        public bool HasDescription
        {
            get { return !string.IsNullOrWhiteSpace(Description); }
        }

        public QualifiedName Name { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0}", Name);
        }

    }
}
