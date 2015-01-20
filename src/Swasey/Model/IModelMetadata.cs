using System;
using System.Linq;

namespace Swasey.Model
{
    public interface IModelMetadata
    {

        IModelMetadata Metadata { get; }

        ServicePath BasePath { get; }

        string Namespace { get; }

        string Version { get; }

    }
}
