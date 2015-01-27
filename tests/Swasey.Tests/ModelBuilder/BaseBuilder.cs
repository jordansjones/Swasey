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
            NewMetadata(new ServicePath(DefaultPath), string.Empty, string.Empty);
        }

        protected IServiceMetadata Metadata { get; private set; }

        private T NewMetadata(ServicePath servicePath = null, string @namespace = null, string version = null)
        {
            Metadata = new ServiceMetadata(
                servicePath ?? Metadata.BasePath, 
                @namespace ?? Metadata.ApiNamespace, 
                version ?? Metadata.Version
                );

            return (T) this;
        }

        public T WithBasePath(string basePath)
        {
            return WithBasePath(new ServicePath(basePath));
        }

        public T WithBasePath(ServicePath basePath)
        {
            return NewMetadata(servicePath : basePath);
        }

        public T WithNamespace(string @namespace)
        {
            return NewMetadata(@namespace : @namespace);
        }

        public T WithVersion(string version)
        {
            return NewMetadata(version : version);
        }


    }
}
