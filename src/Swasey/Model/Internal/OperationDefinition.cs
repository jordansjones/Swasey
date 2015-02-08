using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Swasey.Model
{
    [DebuggerDisplay("{HttpMethod}", Name = "[{ResourceName, nq}] {Name}")]
    internal class OperationDefinition : BaseDefinition, IOperationDefinition
    {

        private ResponseDefinition _response;

        public OperationDefinition(OperationPath path, IServiceMetadata meta) : base(meta)
        {
            Path = path;
            Parameters = new List<ParameterDefinition>();
        }

        public OperationDefinition(IOperationDefinition copyFrom)
            : this(copyFrom.Path, copyFrom.Metadata)
        {
            Context = copyFrom.Context;
            Description = copyFrom.Description;
            HttpMethod = copyFrom.HttpMethod;
            Name = copyFrom.Name;

            copyFrom.Parameters
                .Select(x => new ParameterDefinition(x))
                .ToList()
                .ForEach(x => AddParameter(x));
            SetResponse(new ResponseDefinition(copyFrom.Response));
        }

        public List<ParameterDefinition> Parameters { get; private set; }

        public ResponseDefinition Response
        {
            get { return _response; }
            set
            {
                value.Context = this;
                _response = value;
            }
        }

        public IServiceDefinition Context { get; set; }

        public string Description { get; set; }

        public OperationPath FullPath
        {
            get
            {
                throw new NotImplementedException();
//                return new OperationPath(BasePath, Path);
            }
        }

        public bool HasDescription
        {
            get { return !string.IsNullOrWhiteSpace(Description); }
        }

        public bool HasParameters
        {
            get { return Parameters.Any(); }
        }

        public HttpMethodType HttpMethod { get; set; }

        public QualifiedName Name { get; set; }

        public string Namespace { get { return ApiNamespace; } }

        IReadOnlyList<IParameterDefinition> IOperationDefinition.Parameters
        {
            get { return Parameters; }
        }

        public OperationPath Path { get; private set; }

        public QualifiedName ResourceName { get; set; }

        IResponseDefinition IOperationDefinition.Response
        {
            get { return Response; }
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
