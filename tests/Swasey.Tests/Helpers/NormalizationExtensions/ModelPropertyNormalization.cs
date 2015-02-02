using System;
using System.Linq;

using Swasey.Model;
using Swasey.Normalization;

namespace Swasey.Tests.Helpers
{
    internal static class ModelPropertyNormalization
    {

        public static IModelPropertyDefinition Normalize(this INormalizationApiModelProperty This)
        {
            return new ModelPropertyDefinition(This.AsMetadata())
            {
                Description = This.Description,
                Name = This.Name,
                IsKey = This.IsKey,
                IsRequired = This.IsRequired,
                Type = This.AsDataType()
            };
        }

    }
}
