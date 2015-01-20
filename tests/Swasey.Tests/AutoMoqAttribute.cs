using System;
using System.Linq;

using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit;

namespace Swasey.Tests
{
    public sealed class AutoMoqAttribute : AutoDataAttribute
    {

        public AutoMoqAttribute()
            : base(CreateAutoMoqFixture()) {}


        private static Fixture CreateAutoMoqFixture()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            fixture.Customize<string>(x => new StringGenerator(() => Guid.NewGuid().ToString("N")) );
            return fixture;
        }
    }
}
