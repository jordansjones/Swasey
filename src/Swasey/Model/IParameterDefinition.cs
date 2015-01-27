using System;
using System.Linq;

namespace Swasey.Model
{
    public interface IParameterDefinition : IServiceMetadata
    {

        IOperationDefinition Context { get; }

        DataType DataType { get; }

        bool IsRequired { get; }

        ParameterName Name { get; }

        ParameterType Type { get; }

    }
}
