using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Normalization
{
    internal class NormalizationApiOperation : BaseNormalizationEntity, INormalizationApiOperation
    {

        private readonly List<NormalizationApiOperationParameter> _parameters = new List<NormalizationApiOperationParameter>();

        public NormalizationApiOperation() {}

        public NormalizationApiOperation(INormalizationApiOperation copyFrom) : base(copyFrom)
        {
            BasePath = copyFrom.BasePath;
            Description = copyFrom.Description;
            HttpMethod = copyFrom.HttpMethod;
            Name = copyFrom.Name;
            Path = copyFrom.Path;

            Parameters.AddRange(
                (copyFrom.Parameters ?? new List<INormalizationApiOperationParameter>())
                    .Select(x => new NormalizationApiOperationParameter(x))
                );
        }

        public List<NormalizationApiOperationParameter> Parameters
        {
            get { return _parameters; }
        }

        public string BasePath { get; set; }

        public string Description { get; set; }

        public HttpMethodType HttpMethod { get; set; }

        public string Name { get; set; }

        IReadOnlyList<INormalizationApiOperationParameter> INormalizationApiOperation.Parameters
        {
            get { return Parameters.OfType<INormalizationApiOperationParameter>().ToList(); }
        }

        public string Path { get; set; }

    }
}
