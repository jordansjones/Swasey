using System;
using System.Collections.Generic;
using System.Linq;

using Swasey.Model;
using Swasey.Tests.Helpers;

namespace Swasey.Tests.ModelBuilder
{
    internal class OperationBuilder
    {

        private Action<ResponseBuilder> ResponseBuilderAction { get; set; }
        private readonly List<Action<ParameterBuilder>> _parameterBuilders = new List<Action<ParameterBuilder>>();

        private QualifiedName Name { get; set; }

        private OperationPath Path { get; set; }

        private HttpMethodType? HttpMethod { get; set; }

        public OperationBuilder WithHttpMethod(HttpMethodType type)
        {
            HttpMethod = type;
            return this;
        }

        public OperationBuilder WithName(string name)
        {
            return WithName(new QualifiedName(Ensure.NotNullOrEmpty(name)));
        }

        public OperationBuilder WithName(QualifiedName name)
        {
            Name = Ensure.NotNull(name);
            return this;
        }

        public OperationBuilder WithPath(string path)
        {
            return WithPath(new OperationPath(Ensure.NotNullOrEmpty(path)));
        }

        public OperationBuilder WithPath(OperationPath path)
        {
            Path = Ensure.NotNull(path);
            return this;
        }

        public OperationBuilder WithParameter(Action<ParameterBuilder> action)
        {
            _parameterBuilders.Add(Ensure.NotNull(action));
            return this;
        }

        public OperationBuilder WithResponse(Action<ResponseBuilder> action)
        {
            ResponseBuilderAction = Ensure.NotNull(action);
            return this;
        }

        public OperationDefinition Build(IModelMetadata metadata)
        {
            if (Name == null) throw new ArgumentException("Missing Name");
            if (Path == null) throw new ArgumentException("Missing Path");
            if (ResponseBuilderAction == null) throw new ArgumentException("Missing Response");

            var def = new OperationDefinition(Path, metadata)
            {
                HttpMethod = HttpMethod ?? HttpMethodType.GET,
                Name = Name
            };

            var rb = new ResponseBuilder();
            ResponseBuilderAction(rb);
            def.SetResponse(rb.Build(metadata));

            foreach (var pb in _parameterBuilders)
            {
                var builder = new ParameterBuilder();
                pb(builder);
                def.AddParameter(builder.Build(metadata));
            }

            return def;
        }

    }
}
