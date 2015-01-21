using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    internal class OperationDefinition : BaseDefinition, IOperationDefinition
    {

        private ResponseDefinition _response;

        public OperationDefinition(OperationPath path, IModelMetadata meta) : base(meta)
        {
            Path = path;
            Parameters = new List<IParameterDefinition>();
        }

        public List<IParameterDefinition> Parameters { get; private set; }

        public ResponseDefinition Response
        {
            get { return _response; }
            set
            {
                value.Context = this;
                _response = value;
            }
        }

        public OperationPath Path { get; private set; }

        public OperationPath FullPath
        {
            get
            {
                return new OperationPath(BasePath, Path);
            }
        }

        public IServiceDefinition Context { get; set; }

        public HttpMethodType HttpMethod { get; set; }

        public QualifiedName Name { get; set; }

        public string Description { get; set; }

        public bool HasDescription { get { return !string.IsNullOrWhiteSpace(Description); } }

        public bool HasParameters
        {
            get { return Parameters.Any(); }
        }

        IResponseDefinition IOperationDefinition.Response
        {
            get { return Response; }
        }

        IReadOnlyList<IParameterDefinition> IOperationDefinition.Parameters
        {
            get { return Parameters; }
        }

        public OperationDefinition AddParameter(ParameterDefinition param)
        {
            param.Context = this;
            Parameters.Add(param);

            if (param.Type == ParameterType.Path)
            {
                Path.AddPathParameter(param);
            }

            return this;
        }

        public OperationDefinition SetResponse(ResponseDefinition response)
        {
            response.Context = this;
            Response = response;
            return this;
        }

    }
}
