using System;
using System.Linq;

namespace Swasey.Normalization
{
    internal class NormalizationApiModelProperty : NormalizationApiDataType, INormalizationApiModelProperty
    {

        public NormalizationApiModelProperty() {}

        public NormalizationApiModelProperty(INormalizationApiModelProperty copyFrom)
            : base(copyFrom)
        {
            CopyFrom(copyFrom);

            Name = copyFrom.Name;
            Description = copyFrom.Description;
            IsKey = copyFrom.IsKey;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsKey { get; set; }

    }
}
