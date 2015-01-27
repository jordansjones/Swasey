using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    public interface IModelDefinition : IServiceMetadata
    {

        QualifiedName Name { get; }

        DataType Type { get; }

        IReadOnlyList<IModelPropertyDefinition> Properties { get; }

        bool HasDescription { get; }

        string Description { get; }

    }
}
