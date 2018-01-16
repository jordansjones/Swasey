using System;
using System.Linq;

namespace Swasey.Model
{
    public interface IParameterDefinition : IServiceMetadata
    {

        IOperationDefinitionParent Context { get; }

        DataType DataType { get; }

        bool IsRequired { get; }

        bool IsVariadic { get; }

        ParameterName Name { get; }

        ParameterType Type { get; }

    }
}
