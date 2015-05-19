using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Normalization
{
    internal class NormalizationApiOperation : BaseNormalizationEntity
    {
        private readonly List<NormalizationApiOperationParameter> _parameters = new List<NormalizationApiOperationParameter>();

        public NormalizationApiOperation()
        {
            SupportsStreaming = false;
        }

        public NormalizationApiOperation(NormalizationApiOperation copyFrom) : base(copyFrom)
        {
            BasePath = copyFrom.BasePath;
            Description = copyFrom.Description;
            HttpMethod = copyFrom.HttpMethod;
            Name = copyFrom.Name;
            Path = copyFrom.Path;
            ResourcePath = copyFrom.ResourcePath;
            Response = new NormalizationApiOperationResponse(copyFrom.Response);
            SupportsStreaming = copyFrom.SupportsStreaming;

            if (copyFrom.Parameters.Any())
            {
                Parameters.AddRange(copyFrom.Parameters);
            }
        }

        public List<NormalizationApiOperationParameter> Parameters
        {
            get { return _parameters; }
        }

        public NormalizationApiOperationResponse Response { get; set; }

        public string BasePath { get; set; }

        public string Description { get; set; }

        public HttpMethodType HttpMethod { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string ResourcePath { get; set; }

        public bool SupportsStreaming { get; set; }

    }
}
