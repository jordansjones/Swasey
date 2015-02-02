using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Normalization
{
    public interface INormalizationContext
    {

        IReadOnlyDictionary<string, INormalizationApiModel> Models { get; }
        ILookup<string, INormalizationApiOperation> Operations { get; }

    }
}
