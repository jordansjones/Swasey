using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    internal class ServiceDefinition : BaseDefinition, IServiceDefinition
    {

        public ServiceDefinition(IServiceMetadata meta) : base(meta)
        {
            Operations = new List<OperationDefinition>();
            Models = new List<ModelDefinition>();
        }

        public List<ModelDefinition> Models { get; private set; }

        public List<OperationDefinition> Operations { get; private set; }

        IReadOnlyList<IModelDefinition> IServiceDefinition.Models
        {
            get { return Models; }
        }

        IReadOnlyList<IOperationDefinition> IServiceDefinition.Operations
        {
            get { return Operations; }
        }

        public ServiceDefinition AddModel(ModelDefinition model)
        {
            Models.Add(model);
            return this;
        }

        public ServiceDefinition AddOperation(OperationDefinition operation)
        {
            operation.Context = this;
            Operations.Add(operation);
            return this;
        }

    }
}
