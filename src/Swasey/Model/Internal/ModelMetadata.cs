using System;
using System.Linq;

namespace Swasey.Model
{
    internal class ModelMetadata : IModelMetadata
    {

        public ModelMetadata(ServicePath basePath, string ns, string version)
        {
            BasePath = basePath;
            Namespace = ns;
            Version = version;
        }

        public IModelMetadata Metadata
        {
            get { return this; }
        }

        public ServicePath BasePath { get; private set; }

        public string Namespace { get; private set; }

        public string Version { get; private set; }

    }
}
