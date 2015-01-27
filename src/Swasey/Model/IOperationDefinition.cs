using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    public interface IOperationDefinition : IServiceMetadata
    {

        IServiceDefinition Context { get; }

        HttpMethodType HttpMethod { get; }

        QualifiedName Name { get; }

        bool HasDescription { get; }

        string Description { get; }

        bool HasParameters { get; }

        IReadOnlyList<IParameterDefinition> Parameters { get; }

        OperationPath Path { get; }

        OperationPath FullPath { get; }

        IResponseDefinition Response { get; }

    }
}
