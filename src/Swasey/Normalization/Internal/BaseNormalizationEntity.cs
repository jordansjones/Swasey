using System;
using System.Linq;

namespace Swasey.Normalization
{
    internal abstract class BaseNormalizationEntity : INormalizationEntity
    {

        public BaseNormalizationEntity() {}

        public BaseNormalizationEntity(INormalizationEntity copyFrom)
            : this()
        {
            if (copyFrom == null) return;

            ApiNamespace = copyFrom.ApiNamespace;
            ApiVersion = copyFrom.ApiVersion;
            ModelNamespace = copyFrom.ModelNamespace;
        }

        public string ApiNamespace { get; set; }

        public string ApiVersion { get; set; }

        public string ModelNamespace { get; set; }

    }
}
