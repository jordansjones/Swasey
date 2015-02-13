using System;
using System.Linq;

namespace Swasey.Normalization
{
    internal abstract class NormalizationApiDataType : BaseNormalizationEntity
    {

        public NormalizationApiDataType() {}

        public NormalizationApiDataType(NormalizationApiDataType copyFrom) : base(copyFrom) {}

        public dynamic JObject { get; set; }

        public string DefaultValue { get; set; }

        public bool IsEnum { get; set; }

        public bool IsEnumerable { get; set; }

        public bool IsEnumerableUnique { get; set; }

        public bool IsNullable { get; set; }

        public bool IsRequired { get; set; }

        public bool IsVoidType { get; set; }

        public string[] EnumValues { get; set; }

        public string MinimumValue { get; set; }

        public string MaximumValue { get; set; }

        public string TypeName { get; set; }

        public void SetTypeName(string name)
        {
            TypeName = name;
        }

        internal void CopyFrom(NormalizationApiDataType copyFrom)
        {
            DefaultValue = copyFrom.DefaultValue;
            IsEnum = copyFrom.IsEnum;
            IsEnumerable = copyFrom.IsEnumerable;
            IsEnumerableUnique = copyFrom.IsEnumerableUnique;
            IsNullable = copyFrom.IsNullable;
            IsRequired = copyFrom.IsRequired;
            IsVoidType = copyFrom.IsVoidType;
            EnumValues = copyFrom.EnumValues;
            MinimumValue = copyFrom.MinimumValue;
            MaximumValue = copyFrom.MaximumValue;
            TypeName = copyFrom.TypeName;
        }

    }

}
