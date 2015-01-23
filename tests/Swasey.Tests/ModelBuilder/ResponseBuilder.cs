using System;
using System.Linq;

using Swasey.Model;
using Swasey.Tests.Helpers;

namespace Swasey.Tests.ModelBuilder
{
    internal class ResponseBuilder
    {

        private DataType DataType { get; set; }

        public ResponseBuilder WithVoidDataType()
        {
            return WithDataType(Constants.DataType_Void);
        }

        public ResponseBuilder WithDataType(string type)
        {
            return WithDataType(new DataType(Ensure.NotNullOrEmpty(type)));
        }

        public ResponseBuilder WithDataType(DataType type)
        {
            DataType = Ensure.NotNull(type);
            return this;
        }

        public ResponseDefinition Build(IModelMetadata metadata)
        {
            if (DataType == null) throw new ArgumentException("Missing DataType");

            return new ResponseDefinition(metadata)
            {
                DataType = DataType
            };
        }

    }
}
