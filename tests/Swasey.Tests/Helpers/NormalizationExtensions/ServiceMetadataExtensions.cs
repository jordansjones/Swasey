using System;
using System.Linq;

using Swasey.Model;
using Swasey.Normalization;

namespace Swasey.Tests.Helpers
{
    internal static class ServiceMetadataExtensions
    {

        public static IServiceMetadata AsMetadata(this INormalizationEntity This)
        {
            return new ServiceMetadata(This.ApiNamespace, This.ModelNamespace)
            {
                ApiVersion = This.ApiVersion
            };
        }

    }
}
