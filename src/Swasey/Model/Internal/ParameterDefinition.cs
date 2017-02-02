using System;
using System.Linq;

namespace Swasey.Model
{
    internal class ParameterDefinition : BaseDefinition, IParameterDefinition
    {

        public ParameterDefinition(IServiceMetadata meta) : base(meta) {}

        public ParameterDefinition(IParameterDefinition copyFrom) : base(copyFrom)
        {
            Context = copyFrom.Context;
            DataType = copyFrom.DataType;
            IsRequired = copyFrom.IsRequired;
            IsVariadic = copyFrom.IsVariadic;
            Name = copyFrom.Name;
            Type = copyFrom.Type;
        }

        public IOperationDefinitionParent Context { get; set; }

        public DataType DataType { get; set; }

        public bool IsRequired { get; set; }

        public bool IsVariadic { get; set; }

        public ParameterName Name { get; set; }

        public ParameterType Type { get; set; }

    }
}
