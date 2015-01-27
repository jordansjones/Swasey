using System;
using System.Linq;

namespace Swasey
{
    public class SwaseyException : Exception
    {

        public SwaseyException(string message) : base(message) {}

        public SwaseyException(string messageFormat, params object[] messageArgs) : this(string.Format(messageFormat, messageArgs)) {}

    }
}
