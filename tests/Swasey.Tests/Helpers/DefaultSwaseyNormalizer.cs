using System;
using System.Linq;

using Swasey.Model;
using Swasey.Normalization;

namespace Swasey.Tests.Helpers
{
    internal static class DefaultSwaseyNormalizer
    {

        public static IServiceDefinition Normalize(INormalizationContext context)
        {
            return default(IServiceDefinition);
        }

    }
}
