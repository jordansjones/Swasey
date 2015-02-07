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

        string ResourceName { get; }

        string ResourcePath { get; }

        IReadOnlyList<INormalizationApiModel> SubTypes { get; }

    }
}
