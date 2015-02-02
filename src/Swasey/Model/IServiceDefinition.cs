using System;
using System.Linq;

namespace Swasey.Model
{
    public interface IServiceDefinition
    {

        ILookup<QualifiedName, IOperationDefinition> Operations { get; }

        ILookup<QualifiedName, IModelDefinition> Models { get; }

    }
}
