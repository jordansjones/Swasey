using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    public interface IModelDefinition : IModelEntity
    {

        IReadOnlyList<IModelPropertyDefinition> Properties { get; }

    }
}
