using System;
using System.Linq;

namespace Swasey.Model
{
    internal class ResponseDefinition : BaseDefinition, IResponseDefinition
    {

        public ResponseDefinition(IServiceMetadata meta) : base(meta) {}

        public ResponseDefinition(IResponseDefinition copyFrom) : base(copyFrom)
        {
            Context = copyFrom.Context;
            DataType = copyFrom.DataType;
        }

        public IOperationDefinitionParent Context { get; set; }

        public DataType DataType { get; set; }

    }
}
