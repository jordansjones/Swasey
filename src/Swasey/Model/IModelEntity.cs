using System;
using System.Linq;

namespace Swasey.Model
{
    public interface IModelEntity : IServiceMetadata
    {

        string Description { get; }

        bool HasDescription { get; }

        QualifiedName Name { get; }

    }
}
