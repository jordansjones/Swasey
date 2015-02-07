using System;
using System.Linq;

namespace Swasey.Normalization
{
    public interface INormalizationContext
    {

        ILookup<string, INormalizationApiModel> Models { get; }
        ILookup<string, INormalizationApiOperation> Operations { get; }

    }
}
