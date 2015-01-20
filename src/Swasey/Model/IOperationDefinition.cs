using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    public interface IOperationDefinition : IModelMetadata
    {

        IServiceDefinition Context { get; }

        HttpMethodType HttpMethod { get; }

        QualifiedName Name { get; }

        bool HasParameters { get; }

        IReadOnlyList<IParameterDefinition> Parameters { get; }

        OperationPath Path { get; }

        OperationPath FullPath { get; }

        IResponseDefinition Response { get; }

    }
}
