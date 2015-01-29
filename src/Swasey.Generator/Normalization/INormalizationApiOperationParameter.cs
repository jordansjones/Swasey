using System;
using System.Linq;

using Swasey.Model;

namespace Swasey.Normalization
{
    public interface INormalizationApiOperationParameter : INormalizationEntity, INormalizationApiDataType
    {

        string Description { get; }

        string Name { get; }

        ParameterType ParameterType { get; }

    }
}
