using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Swasey.Model;
using Swasey.Normalization;

namespace Swasey.Lifecycle
{
    internal interface ILifecycleContext
    {

        Action<TextWriter, object> ApiEnumTemplate { get; }

        Action<TextWriter, object> ApiModelTemplate { get; }

        string ApiNamespace { get; }

        Action<TextWriter, object> ApiOperationTemplate { get; }

        IReadOnlyCollection<KeyValuePair<string, dynamic>> ApiPathJsonMapping { get; }

        SwaseyEnumWriter EnumWriter { get; }

        SwaggerJsonLoader Loader { get; }

        string ModelNamespace { get; }

        SwaseyModelWriter ModelWriter { get; }

        NormalizationContext NormalizationContext { get; }

        SwaseyOperationWriter OperationWriter { get; }

        dynamic ResourceListingJson { get; }

        Uri ResourceListingUri { get; }

        IServiceDefinition ServiceDefinition { get; }

        IServiceMetadata ServiceMetadata { get; }

        LifecycleState State { get; }

        string SwaggerVersion { get; }

    }
}
