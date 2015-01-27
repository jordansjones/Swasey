using System;
using System.Linq;

using Swasey.Model;

namespace Swasey.Tests.ModelBuilder
{
    internal abstract class BaseBuilder<T>
        where T : BaseBuilder<T>
    {

        private const string DefaultPath = "/";

        protected BaseBuilder()
        {
            NewMetadata(Defaults.DefaultApiNamespace, Defaults.DefaultModelNamespace, string.Empty);
        }

        protected IServiceMetadata Metadata { get; private set; }

        private T NewMetadata(string apiNamespace = null, string modelNamespace = null, string apiVersion = null)
        {
            Metadata = new ServiceMetadata(
                apiNamespace ?? Metadata.ApiNamespace,
                modelNamespace ?? Metadata.ModelNamespace
                )
            {
                ApiVersion = apiVersion ?? Metadata.ApiVersion
            };

            return (T) this;
        }

        public T WithApiNamespace(string @namespace)
        {
            return NewMetadata(apiNamespace : @namespace);
        }

        public T WithModelNamespace(string @namespace)
        {
            return NewMetadata(modelNamespace : @namespace);
        }

        public T WithVersion(string version)
        {
            return NewMetadata(apiVersion : version);
        }


    }
}
