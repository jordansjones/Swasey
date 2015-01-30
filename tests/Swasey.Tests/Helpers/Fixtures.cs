using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Jil;

using Ploeh.AutoFixture;

using Swasey.Tests.Schema.Version12;

namespace Swasey.Tests.Helpers
{
    internal static class Fixtures
    {

        public static readonly Uri TestResourceListingUri = new Uri("http://localtest");

        public static SingleFixtureBuilder Build
        {
            get { return new SingleFixtureBuilder(CreateAutoFixture()); }
        }

        public static T Create<T>()
        {
            return Build.One.Of<T>();
        }

        public static T Create<T>(T seed)
        {
            return Build.One.Of(seed);
        }

        public static Fixture CreateAutoFixture()
        {
            var fixture = new Fixture();
            fixture.Customize<string>(x => new StringGenerator(() => Guid.NewGuid().ToString("N")));
            return fixture;
        }

        public static Task<string> CreateResourceListingJson()
        {
            return DefaultSwaggerJsonCreator.CreateResourceListingJson();
        }


    }
}
