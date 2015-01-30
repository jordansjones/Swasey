using System;
using System.Linq;

namespace Swasey.Normalization
{
    internal class NormalizationApiOperationResponse : NormalizationApiDataType, INormalizationApiOperationResponse
    {

        public NormalizationApiOperationResponse() {}

        public NormalizationApiOperationResponse(INormalizationApiOperationResponse copyFrom) : base(copyFrom)
        {
            if (copyFrom == null) return;
            CopyFrom(copyFrom);
        }

    }
}
