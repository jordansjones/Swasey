using System;
using System.Linq;

using Swasey.Model;
using Swasey.Normalization;

namespace Swasey
{
    internal static class DataTypeExtensions
    {

        public static DataType AsDataType(this NormalizationApiDataType This)
        {
            return new DataType(This.TypeName.MapDataTypeName())
            {
                DefaultValue = This.DefaultValue,
                IsEnum = This.IsEnum,
                IsEnumerable = This.IsEnumerable,
                IsEnumerableUnique = This.IsEnumerableUnique,
                IsModelType = This.IsModelType,
                IsPrimitive = This.IsPrimitive,
                MaximumValue = This.MaximumValue,
                MinimumValue = This.MinimumValue
            };
        }

        public static string MapDataTypeName(this string This)
        {
            string realValue;
            if (!Constants.DataTypeMapping.TryGetValue(This, out realValue))
            {
                realValue = This;
            }
            return realValue;
        }

    }
}
