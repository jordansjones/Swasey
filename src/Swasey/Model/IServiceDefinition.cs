using System;
using System.Linq;

namespace Swasey.Model
{
    public interface IServiceDefinition
    {

        ILookup<QualifiedName, IModelEntity> Entities { get; }

        ILookup<QualifiedName, IEnumDefinition> Enums { get; }

        ILookup<QualifiedName, IModelDefinition> Models { get; }

        ILookup<QualifiedName, IOperationDefinition> ResourceOperations { get; }
        ILookup<QualifiedName, IOperationDefinition20> ResourceOperations20 { get; }


    }
}
