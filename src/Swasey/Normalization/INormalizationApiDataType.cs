using System;
using System.Linq;

namespace Swasey.Normalization
{
    public interface INormalizationApiDataType
    {

        string DefaultValue { get; }

        bool IsEnum { get; }

        bool IsEnumerable { get; }

        bool IsEnumerableUnique { get; }

        bool IsNullable { get; }

        bool IsRequired { get; }

        bool IsVoidType { get; }

        string[] EnumValues { get; }

        string MinimumValue { get; }

        string MaximumValue { get; }

        string TypeName { get; }

    }
}
