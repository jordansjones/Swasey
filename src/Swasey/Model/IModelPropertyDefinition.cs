using System;
using System.Linq;

namespace Swasey.Model
{
    public interface IModelPropertyDefinition : IServiceMetadata
    {
        QualifiedName Name { get; }

        DataType Type { get; }

        bool CanBeObserved { get; }

        string Description { get; }

        bool HasDescription { get; }

        bool IsKey { get; }

        bool IsRequired { get; }

    }
}
