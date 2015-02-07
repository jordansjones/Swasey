using System;
using System.Linq;

namespace Swasey.Model
{
    public interface IServiceMetadata
    {

        string ApiNamespace { get; }

        string ApiVersion { get; }

        IServiceMetadata Metadata { get; }

        string ModelNamespace { get; }

    }
}
