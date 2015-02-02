using System;
using System.Linq;

using Swasey.Model;
using Swasey.Normalization;

namespace Swasey.Tests.Helpers
{
    internal static class ModelNormalization
    {

        public static ModelDefinition Normalize(this INormalizationApiModel This)
        {
            var def = new ModelDefinition(This.AsMetadata())
            {
                Description = This.Description,
                Name = This.Name
            };

            def.Properties.AddRange(This.Properties.Values.Select(x => x.Normalize()));

            return def;
        }

    }
}
