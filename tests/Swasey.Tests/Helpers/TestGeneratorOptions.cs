using System;
using System.Linq;

namespace Swasey.Tests.Helpers
{
    internal class TestGeneratorOptions : GeneratorOptions
    {

        private readonly TestSwaggerJsonLoader _jsonLoader;

        public TestGeneratorOptions(TestSwaggerJsonLoader jsonLoader)
        {
            _jsonLoader = jsonLoader;

            Loader = _jsonLoader.Load;
        }

    }
}
