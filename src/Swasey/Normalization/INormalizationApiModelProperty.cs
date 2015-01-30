using System;
using System.Linq;

namespace Swasey.Normalization
{

    public interface INormalizationApiModelProperty : INormalizationEntity, INormalizationApiDataType
    {

        string Name { get; }

        string Description { get; }

        bool IsKey { get; }

    }

}
