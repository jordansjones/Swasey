using System;
using System.Linq;

namespace Swasey.Model
{
    public interface IParameterDefinition : IModelMetadata
    {

        IOperationDefinition Context { get; }

        DataType DataType { get; }

        bool IsRequired { get; }

        ParameterName Name { get; }

        ParameterType Type { get; }

    }
}
