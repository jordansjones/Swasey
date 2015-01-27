using System;
using System.Linq;

namespace Swasey.Model
{
    internal class ParameterDefinition : BaseDefinition, IParameterDefinition
    {

        public ParameterDefinition(IServiceMetadata meta) : base(meta) {}

        public IOperationDefinition Context { get; set; }

        public DataType DataType { get; set; }

        public bool IsRequired { get; set; }

        public ParameterName Name { get; set; }

        public ParameterType Type { get; set; }

    }
}
