using System;
using System.Linq;

namespace Swasey.Model
{
    internal abstract class BaseDefinition : IModelMetadata
    {

        protected BaseDefinition(IModelMetadata meta)
        {
            Metadata = meta;
        }

        public IModelMetadata Metadata { get; private set; }

        public ServicePath BasePath
        {
            get { return Metadata.BasePath; }
        }

        public string Namespace
        {
            get { return Metadata.Namespace; }
        }

        public string Version
        {
            get { return Metadata.Version; }
        }


    }
}
