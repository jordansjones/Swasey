using System;
using System.Linq;

using Swasey.Model;

namespace Swasey.Normalization
{
    internal class NormalizationApiOperationParameter : NormalizationApiDataType, INormalizationApiOperationParameter
    {

        public NormalizationApiOperationParameter()
        {
            AllowsMultiple = false;
        }

        public NormalizationApiOperationParameter(INormalizationApiOperationParameter copyFrom) : base(copyFrom)
        {
            if (copyFrom == null) return;

            CopyFrom(copyFrom);
            AllowsMultiple = copyFrom.AllowsMultiple;
            Description = copyFrom.Description;
            Name = copyFrom.Name;
            ParameterType = copyFrom.ParameterType;
        }

        public bool AllowsMultiple { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public ParameterType ParameterType { get; set; }

    }
}
