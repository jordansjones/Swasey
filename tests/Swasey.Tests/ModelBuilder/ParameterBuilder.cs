using System;
using System.Linq;

using Swasey.Model;
using Swasey.Tests.Helpers;

namespace Swasey.Tests.ModelBuilder
{
    internal class ParameterBuilder
    {

        private DataType DataType { get; set; }

        private ParameterName Name { get; set; }

        public ParameterType? Type { get; set; }

        public ParameterBuilder WithDataType(string type)
        {
            return WithDataType(new DataType(Ensure.NotNullOrEmpty(type)));
        }

        public ParameterBuilder WithDataType(DataType type)
        {
            DataType = Ensure.NotNull(type);
            return this;
        }

        public ParameterBuilder WithName(string name)
        {
            return WithName(new ParameterName(Ensure.NotNullOrEmpty(name)));
        }

        public ParameterBuilder WithName(ParameterName name)
        {
            Name = Ensure.NotNull(name);
            return this;
        }

        public ParameterBuilder WithType(ParameterType type)
        {
            Type = type;
            return this;
        }


        public ParameterDefinition Build(IModelMetadata metadata)
        {
            if (Name == null) throw new ArgumentException("Missing Name");
            if (DataType == null) throw new ArgumentException("Missing DataType");

            return new ParameterDefinition(metadata)
            {
                Name = Name,
                Type = Type ?? ParameterType.Body
            };
        }

    }
}