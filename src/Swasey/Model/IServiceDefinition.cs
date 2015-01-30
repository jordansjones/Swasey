using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    public interface IServiceDefinition : IServiceMetadata
    {

        IReadOnlyList<IOperationDefinition> Operations { get; }

        IReadOnlyList<IModelDefinition> Models { get; }

    }
}
