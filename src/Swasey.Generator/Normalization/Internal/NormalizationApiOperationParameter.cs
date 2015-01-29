using System;
using System.Linq;

using Swasey.Model;

namespace Swasey.Normalization
{
    internal class NormalizationApiOperationParameter : NormalizationApiDataType, INormalizationApiOperationParameter
    {

        public NormalizationApiOperationParameter() {}

        public NormalizationApiOperationParameter(INormalizationApiOperationParameter copyFrom) : base(copyFrom)
        {
            CopyFrom(copyFrom);
            Description = copyFrom.Description;
            Name = copyFrom.Name;
            ParameterType = copyFrom.ParameterType;
        }

        public string Description { get; set; }

        public string Name { get; set; }

        public ParameterType ParameterType { get; set; }

    }
}
