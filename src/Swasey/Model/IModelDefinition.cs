using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    public interface IModelDefinition : IModelEntity
    {

        IReadOnlyList<IModelPropertyDefinition> DefaultValueProperties { get; }

        bool HasDefaultValueProperties { get; }

        bool HasKeyProperty { get; }

        bool HasMaximumValueProperties { get; }

        bool HasMinimumValueProperties { get; }

        bool HasRequiredProperties { get; }

        IModelPropertyDefinition KeyProperty { get; }

        IReadOnlyList<IModelPropertyDefinition> MaximumValueProperties { get; }

        IReadOnlyList<IModelPropertyDefinition> MinimumValueProperties { get; }

        IReadOnlyList<IModelPropertyDefinition> Properties { get; }

        IReadOnlyList<IModelPropertyDefinition> RequiredProperties { get; }

    }
}
