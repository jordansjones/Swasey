using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    internal class ServiceDefinition : BaseDefinition, IServiceDefinition
    {

        public ServiceDefinition(IServiceMetadata meta) : base(meta)
        {
            Operations = new List<IOperationDefinition>();
        }

        public List<IOperationDefinition> Operations { get; private set; }

        public bool HasCountOperation
        {
            get { throw new NotImplementedException(); }
        }

        public bool HasCreateOperation
        {
            get { throw new NotImplementedException(); }
        }

        public bool HasDeleteOperation
        {
            get { throw new NotImplementedException(); }
        }

        public bool HasGetByKeyOperation
        {
            get { throw new NotImplementedException(); }
        }

        public bool HasUpdateOperation
        {
            get { throw new NotImplementedException(); }
        }

        public QualifiedName Name { get; set; }

        IReadOnlyList<IOperationDefinition> IServiceDefinition.Operations
        {
            get { return Operations; }
        }

        public ServiceDefinition AddOperation(OperationDefinition operation)
        {
            operation.Context = this;
            Operations.Add(operation);
            return this;
        }

    }
}
