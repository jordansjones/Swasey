using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    public interface IOperationDefinition : IServiceMetadata
    {

        IServiceDefinition Context { get; }

        string Description { get; }

        OperationPath FullPath { get; }

        bool HasDescription { get; }

        bool HasParameters { get; }

        HttpMethodType HttpMethod { get; }

        QualifiedName Name { get; }

        IReadOnlyList<IParameterDefinition> Parameters { get; }

        OperationPath Path { get; }

        QualifiedName ResourceName { get; }

        IResponseDefinition Response { get; }

    }
}
