using System;
using System.Linq;
using System.Text;

using Swasey.Model;

namespace Swasey.Tests.Helpers
{
    public class StringBuilderSwaseyWriter : ITestSwaseyWriter
    {

        public StringBuilderSwaseyWriter(StringBuilder builder = null)
        {
            Builder = builder ?? new StringBuilder();
        }

        public StringBuilder Builder { get; private set; }

        public void Write(string name, string content, IOperationDefinitionParent definition)
        {
            Builder.Append(content);
        }

        public void Write(string name, string content, IEnumDefinition definition)
        {
            Builder.Append(content);
        }

        public void Write(string name, string content, IModelDefinition definition)
        {
            Builder.Append(content);
        }

    }
}
