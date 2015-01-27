using System;
using System.Linq;

namespace Swasey.Model
{
    internal class ResponseDefinition : BaseDefinition, IResponseDefinition
    {

        public ResponseDefinition(IServiceMetadata meta) : base(meta) {}

        public IOperationDefinition Context { get; set; }

        public DataType DataType { get; set; }

    }
}
