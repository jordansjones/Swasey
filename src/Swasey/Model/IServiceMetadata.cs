using System;
using System.Linq;

namespace Swasey.Model
{
    public interface IServiceMetadata
    {

        IServiceMetadata Metadata { get; }

        string ApiNamespace { get; }

        string ModelNamespace { get; }

        string ApiVersion { get; }

    }
}
