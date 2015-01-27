using System;
using System.Linq;

namespace Swasey.Model
{
    internal abstract class BaseDefinition : IServiceMetadata
    {

        protected BaseDefinition(IServiceMetadata meta)
        {
            ApiNamespace = meta.ApiNamespace;
            ModelNamespace = meta.ModelNamespace;
            ApiVersion = meta.ApiVersion;
        }

        public IServiceMetadata Metadata { get { return this; } }

        public string ApiNamespace { get; private set; }

        public string ModelNamespace { get; private set; }

        public string ApiVersion { get; set; }


    }
}
