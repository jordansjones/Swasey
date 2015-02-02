using System;
using System.Linq;

using Swasey.Model;
using Swasey.Normalization;

namespace Swasey.Tests.Helpers
{
    internal static class DataTypeNormalization
    {

        public static DataType AsDataType(this INormalizationApiDataType This)
        {
            return new DataType(This.TypeName)
            {
                DefaultValue = This.DefaultValue,
                EnumValues = This.EnumValues,
                IsEnum = This.IsEnum,
                IsEnumerable = This.IsEnumerable,
                IsEnumerableUnique = This.IsEnumerableUnique,
                IsNullable = This.IsNullable,
                MaximumValue = This.MaximumValue,
                MinimumValue = This.MinimumValue
            };
        }

    }
}
