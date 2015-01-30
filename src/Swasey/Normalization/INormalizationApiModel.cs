using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Normalization
{
    public interface INormalizationApiModel : INormalizationEntity
    {

        string Description { get; }

        string Discriminator { get; }

        string Name { get; }

        IReadOnlyDictionary<string, INormalizationApiModelProperty> Properties { get; }

        IReadOnlyList<INormalizationApiModel> SubTypes { get; }

    }
}
