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
            : base(new Fixture()
                       .Customize(new AutoMoqCustomization())) {}

    }
}
