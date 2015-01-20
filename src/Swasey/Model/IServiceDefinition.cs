using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    public interface IServiceDefinition : IModelMetadata
    {

        bool HasCountOperation { get; }

        bool HasCreateOperation { get; }

        bool HasDeleteOperation { get; }

        bool HasGetByKeyOperation { get; }

        bool HasUpdateOperation { get; }

        QualifiedName Name { get; }

        IReadOnlyList<IOperationDefinition> Operations { get; }

    }
}
