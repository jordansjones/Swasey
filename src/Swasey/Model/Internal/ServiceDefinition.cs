using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    internal class ServiceDefinition : IServiceDefinition
    {

        public ServiceDefinition()
        {
            Operations = new List<OperationDefinition>();
            Entities = new List<BaseModelEntityDefinition>();
        }

        public ServiceDefinition(IServiceDefinition copyFrom)
            : this()
        {
            CopyFrom(copyFrom);
        }

        public List<BaseModelEntityDefinition> Entities { get; private set; }

        public List<OperationDefinition> Operations { get; private set; }

        ILookup<QualifiedName, IModelEntity> IServiceDefinition.Entities
        {
            get { return Entities.OfType<IModelEntity>().ToLookup(x => x.Name); }
        }

        ILookup<QualifiedName, IEnumDefinition> IServiceDefinition.Enums
        {
            get { return Entities.OfType<IEnumDefinition>().ToLookup(x => x.Name); }
        }

        ILookup<QualifiedName, IModelDefinition> IServiceDefinition.Models
        {
            get { return Entities.OfType<IModelDefinition>().ToLookup(x => x.Name); }
        }

        ILookup<QualifiedName, IOperationDefinition> IServiceDefinition.ResourceOperations
        {
            get { return Operations.OfType<IOperationDefinition>().ToLookup(x => x.ResourceName); }
        }

        public ServiceDefinition AddEnum(EnumDefinition @enum)
        {
            Entities.Add(@enum);
            return this;
        }

        public ServiceDefinition AddModel(ModelDefinition model)
        {
            Entities.Add(model);
            return this;
        }

        public ServiceDefinition AddOperation(OperationDefinition operation)
        {
            operation.Context = this;
            Operations.Add(operation);
            return this;
        }

        public void CopyFrom(IServiceDefinition copyFrom)
        {
            if (copyFrom == null) { return; }

            var svcDef = copyFrom as ServiceDefinition;
            if (svcDef != null)
            {
                Entities.AddRange(svcDef.Entities);
                Operations.AddRange(svcDef.Operations);
                return;
            }


            Entities.AddRange(
                copyFrom.Enums
                    .SelectMany(x => x)
                    .Select(x => new EnumDefinition(x))
                );

            Entities.AddRange(
                copyFrom.Models
                    .SelectMany(x => x)
                    .Select(x => new ModelDefinition(x))
                );

            Operations.AddRange(
                copyFrom.ResourceOperations
                    .SelectMany(x => x)
                    .Select(x => new OperationDefinition(x))
                );
        }

    }
}
