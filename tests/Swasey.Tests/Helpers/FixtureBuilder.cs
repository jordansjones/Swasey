using System;
using System.Collections.Generic;
using System.Linq;

using Ploeh.AutoFixture;

namespace Swasey.Tests.Helpers
{
    internal class SingleFixtureBuilder : BaseFixtureBuilder
    {
        public SingleFixtureBuilder(Fixture fixture) : base(fixture) {}

        public T Of<T>()
        {
            return Fixture.Create<T>();
        }

        public T Of<T>(T seed)
        {
            return Fixture.Create(seed);
        }

    }

    internal class MultiFixtureBuilder : BaseFixtureBuilder
    {

        public MultiFixtureBuilder(Fixture fixture) : base(fixture) {}

        public IEnumerable<T> Of<T>()
        {
            return Fixture.CreateMany<T>();
        }

        public IEnumerable<T> Of<T>(T seed)
        {
            return Fixture.CreateMany(seed);
        }

    }

    internal abstract class BaseFixtureBuilder
    {
        private readonly Fixture _fixture;

        protected BaseFixtureBuilder(Fixture fixture)
        {
            _fixture = fixture;
            Count = 1;
        }

        protected Fixture Fixture { get { return _fixture; } }

        protected int Count { get; private set; }

        #region Counts

        public SingleFixtureBuilder One
        {
            get { return new SingleFixtureBuilder(Fixture); }
        }

        public MultiFixtureBuilder Two
        {
            get
            {
                return Lots(2);
            }
        }

        public MultiFixtureBuilder Three
        {
            get
            {
                return Lots(3);
            }
        }

        public MultiFixtureBuilder Four
        {
            get
            {
                return Lots(4);
            }
        }

        public MultiFixtureBuilder Five
        {
            get
            {
                return Lots(5);
            }
        }

        public MultiFixtureBuilder Six
        {
            get
            {
                return Lots(6);
            }
        }

        public MultiFixtureBuilder Seven
        {
            get
            {
                return Lots(7);
            }
        }

        public MultiFixtureBuilder Eight
        {
            get
            {
                return Lots(8);
            }
        }

        public MultiFixtureBuilder Nine
        {
            get
            {
                return Lots(9);
            }
        }

        public MultiFixtureBuilder Ten
        {
            get
            {
                return Lots(10);
            }
        }

        #endregion

        public MultiFixtureBuilder Lots(int count)
        {
            // ReSharper disable once LocalizableElement
            if (count < 1) throw new ArgumentException("Can't create less than 1", "count");
            Count = count;

            return this is MultiFixtureBuilder
                ? (MultiFixtureBuilder) this
                : new MultiFixtureBuilder(Fixture)
                {
                    Count = Count
                };
        }

    }
}
