using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Normalization
{
    public interface INormalizationApiOperation : INormalizationEntity
    {

        string BasePath { get; }

        string Description { get; }

        HttpMethodType HttpMethod { get; }

        string Name { get; }

        IReadOnlyList<INormalizationApiOperationParameter> Parameters { get; }

        string Path { get; }

    }
}
