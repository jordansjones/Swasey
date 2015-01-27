using System;
using System.Linq;

namespace Swasey.Model
{
    internal class ModelPropertyDefinition : BaseDefinition, IModelPropertyDefinition
    {

        public ModelPropertyDefinition(IServiceMetadata meta) : base(meta) {}

        public QualifiedName Name { get; set; }

        public DataType Type { get; set; }

        public bool HasDescription
        {
            get { return !string.IsNullOrWhiteSpace(Description); }
        }

        public string Description { get; set; }

    }
}
