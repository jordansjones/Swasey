using System;
using System.Linq;

namespace Swasey.Model
{
    public interface IModelPropertyDefinition
    {
        QualifiedName Name { get; }

        DataType Type { get; }

        bool HasDescription { get; }

        string Description { get; }

    }
}
