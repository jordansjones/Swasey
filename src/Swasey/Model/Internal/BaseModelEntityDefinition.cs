using System;
using System.Linq;

namespace Swasey.Model
{
    internal abstract class BaseModelEntityDefinition : BaseDefinition, IModelEntity
    {

        protected BaseModelEntityDefinition(IServiceMetadata meta) : base(meta) {}

        protected BaseModelEntityDefinition(IModelEntity copyFrom) : base(copyFrom)
        {
            Description = copyFrom.Description;
            Name = copyFrom.Name;
            ResourceName = copyFrom.ResourceName;
        }

        public string Description { get; set; }

        public bool HasDescription
        {
            get { return !string.IsNullOrWhiteSpace(Description); }
        }

        public QualifiedName Name { get; set; }

        public string Namespace
        {
            get { return ModelNamespace; }
        }

        public QualifiedName ResourceName { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0}", Name);
        }

    }
}
