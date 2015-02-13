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
            return new DataType(This.TypeName)
            {
                DefaultValue = This.DefaultValue,
                IsEnumerable = This.IsEnumerable,
                IsEnumerableUnique = This.IsEnumerableUnique,
                MaximumValue = This.MaximumValue,
                MinimumValue = This.MinimumValue
            };
        }

    }
}
