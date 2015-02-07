using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    public interface IEnumDefinition : IModelEntity
    {

        IReadOnlyList<KeyValuePair<string, int>> Values { get; }

    }
}
