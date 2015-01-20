using System;
using System.Linq;

using Jil;

using Swasey.Schema.Version12;
using Swasey.Tests.Helpers;

using Xunit;

namespace Swasey.Tests.Schema.Version12
{
    public class ApiDefinitionTests
    {

        [Fact]
        public void TestDesrializeDynamic()
        {
            var json = GhettoJsonCreator.Object(o => o
                .Object("Id", id => id.Value("type", "integer").Value("format", "int32"))
                .Object("Name", name => name.Value("type", "string"))
            );

            var result = JSON.Deserialize<DtoProperties>(json);
            var first = result.Values.First();
            var hasType = first.ContainsKey("type");
            var db = true;
        }

    }
}
