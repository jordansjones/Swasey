using System;
using System.Collections.Generic;
using System.Linq;

using Swasey.Model;
using Swasey.Normalization;

namespace Swasey.Lifecycle
{
    public interface ILifecycleContext
    {
        Uri ResourceListingUri { get; }
        IServiceMetadata ServiceMetadata { get; }

        string ApiNamespace { get; }
        string ModelNamespace { get; }

        string SwaggerVersion { get; }

        SwaggerJsonLoader Loader { get; }
        SwaseyWriter Writer { get; }

        LifecycleState State { get; }

        dynamic ResourceListingJson { get; }

        IReadOnlyCollection<KeyValuePair<string, dynamic>> ApiPathJsonMapping { get; }

        INormalizationContext NormalizationContext { get; }

        IReadOnlyCollection<KeyValuePair<QualifiedName, IModelDefinition>> Models { get; }

    }
}
