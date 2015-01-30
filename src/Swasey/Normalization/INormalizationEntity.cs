using System;
using System.Linq;

namespace Swasey.Normalization
{
    public interface INormalizationEntity
    {

        string ApiNamespace { get; }

        string ApiVersion { get; }

        string ModelNamespace { get; }

    }
}
