using System;
using System.Linq;

namespace Swasey.Tests.Helpers
{
    internal class TestGeneratorOptions : GeneratorOptions
    {

        private readonly TestSwaggerJsonLoader _jsonLoader;

        private readonly ITestSwaseyWriter _testWriter;

        public TestGeneratorOptions(TestSwaggerJsonLoader jsonLoader, ITestSwaseyWriter writer)
        {
            _jsonLoader = jsonLoader;
            _testWriter = writer;

            Loader = _jsonLoader.Load;
            OperationWriter = _testWriter.Write;
        }

    }
}
