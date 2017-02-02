using System;
using System.Linq;

using Swasey.Model;

namespace Swasey.Tests.Helpers
{
    public interface ITestSwaseyWriter
    {

        void Write(string name, string content, IOperationDefinitionParent definition);

        void Write(string name, string content, IEnumDefinition definition);

        void Write(string name, string content, IModelDefinition definition);

    }
}
