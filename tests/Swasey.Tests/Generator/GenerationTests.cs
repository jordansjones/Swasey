using System;
using System.Linq;
using System.Threading.Tasks;

using Swasey.Tests.Helpers;

using Xunit;

namespace Swasey.Tests.Generator
{
    public class GenerationTests
    {

        [Fact]
        public async Task GenertorWorks()
        {
            var gen = new SwaseyGenerator(GenerationTestHelper.DefaultGeneratorOptions());

            await gen.Generate(Fixtures.TestResourceListingUri);
        }

    }
}
