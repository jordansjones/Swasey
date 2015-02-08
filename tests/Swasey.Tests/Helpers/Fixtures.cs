using System;
using System.Linq;
using System.Threading.Tasks;

using Ploeh.AutoFixture;

namespace Swasey.Tests.Helpers
{
    internal static class Fixtures
    {

        public const string DefaultApiNamespace = "Swasey.Service.Client.Api";

        public const string DefaultBasePath = "/";

        public const string DefaultModelNamespace = "Swasey.Service.Client.Model";

        public const string DefaultVersion = "1";

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
