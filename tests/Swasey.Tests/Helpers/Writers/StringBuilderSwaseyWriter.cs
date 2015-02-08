using System;
using System.Linq;
using System.Text;

namespace Swasey.Tests.Helpers
{
    public class StringBuilderSwaseyWriter : ITestSwaseyWriter
    {

        public StringBuilderSwaseyWriter(StringBuilder builder = null)
        {
            Builder = builder ?? new StringBuilder();
        }

        public StringBuilder Builder { get; private set; }

        public void Write(WriteType type, string name, string content)
        {
            Builder.Append(content);
        }


    }
}
