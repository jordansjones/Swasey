using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    public interface IOperationDefinitionParent : IServiceMetadata
    {
        IServiceDefinition Context { get; }

        QualifiedName Name { get; }

        QualifiedName ResourceName { get; }

        IResponseDefinition Response { get; }

    }
}
