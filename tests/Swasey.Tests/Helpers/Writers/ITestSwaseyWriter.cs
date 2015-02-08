using System;
using System.Linq;

namespace Swasey.Tests.Helpers
{
    public interface ITestSwaseyWriter
    {

        void Write(WriteType type, string name, string content);

    }
}
