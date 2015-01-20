using System;
using System.Collections.Generic;
using System.Linq;

using Swasey.Model;
using Swasey.Tests.Helpers;

namespace Swasey.Tests.ModelBuilder
{
    internal class ServiceBuilder : BaseBuilder<ServiceBuilder>
    {

        private readonly List<Action<OperationBuilder>> _operationBuilders = new List<Action<OperationBuilder>>();

        private QualifiedName Name { get; set; }

        public ServiceBuilder WithName(string name)
        {
            return WithName(new QualifiedName(Ensure.NotNullOrEmpty(name)));
        }

        public ServiceBuilder WithName(QualifiedName name)
        {
            Name = Ensure.NotNull(name);
            return this;
        }

        public ServiceBuilder WithOperation(Action<OperationBuilder> action)
        {
            _operationBuilders.Add(Ensure.NotNull(action));
            return this;
        }


        public IServiceDefinition Build()
        {
            var def = new ServiceDefinition(Metadata)
            {
                Name = Name
            };

            foreach (var op in _operationBuilders)
            {
                var opBuilder = new OperationBuilder();
                op(opBuilder);
                def.AddOperation(opBuilder.Build(Metadata));
            }

            return def;
        }

    }
}
