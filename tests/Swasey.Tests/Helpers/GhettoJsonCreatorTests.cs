using System;
using System.Collections.Generic;

using FluentAssertions;

using System.Linq;
using System.Text;

using Ploeh.AutoFixture;

using Xunit;
using Xunit.Extensions;

namespace Swasey.Tests.Helpers
{
    public class GhettoJsonCreatorTests
    {

        [Fact]
        public void TestSimpleValue()
        {
            const string expected = "test123";

            GhettoJsonCreator.Value(expected)
                .Should()
                .NotBeNullOrWhiteSpace("because encoding a valid string doesn't return null or whitespace")
                .And.StartWith("\"", "because json encoded strings start with a '{0}'", '"')
                .And.EndWith("\"", "because json encoded strings end with a '{0}", '"')
                .And.Be(string.Format("{0}{1}{0}", '"', expected));
        }

        [Fact]
        public void TestSimpleQuotedValue()
        {
            const string expected = "1\"2\"3";

            var result = GhettoJsonCreator.Value(expected);

            result[0].Should().Be('"');
            result[1].Should().Be('1');
            result[2].Should().Be('\\');
            result[3].Should().Be('"');
            result[4].Should().Be('2');
            result[5].Should().Be('\\');
            result[6].Should().Be('"');
            result[7].Should().Be('3');
            result[8].Should().Be('"');
        }

        [Theory, AutoMoq]
        public void TestSimpleArray()
        {
            var items = new List<int> {1, 2, 4, 3};

            var result = GhettoJsonCreator.Array(
                x =>
                {
                    items.ForEach(y => x.Value(y));
                });

            result.Should()
                .Be(string.Format("[{0}]", string.Join(",", items)));
        }

        [Fact]
        public void TestSimpleObject()
        {
            var key = "TestKey";
            var value = "TestValue";

            var result = GhettoJsonCreator.Object(x => x.Value(key, value));

            result.Should()
                .Be(
                new StringBuilder()
                    .Append('{')
                        .Append('"').Append(key).Append('"')
                        .Append(':')
                        .Append('"').Append(value).Append('"')
                    .Append('}')
                    .ToString()
                );
        }

        [Theory, AutoMoq]
        public void TestComplexArray(Generator<string> stringGen, Generator<int> intGen)
        {
            var count = Rand(2, 5);
            var items = new Dictionary<string, object>[count];

            var sb = new StringBuilder().Append('[');
            for (var i = 0; i < count; i++)
            {
                items[i] = DictionaryGen(stringGen, intGen);
                sb.Append(Encode(items[i]))
                    .Append((i + 1) < count ? "," : "");
            }
            sb.Append(']');

            var expected = sb.ToString();

            var result = GhettoJsonCreator.Array(
                x =>
                {
                    foreach (var dict in items)
                    {
                        x.Object(
                            o =>
                            {
                                foreach (var kv in dict)
                                {
                                    if (kv.Value is int)
                                    {
                                        o.Value(kv.Key, (int) kv.Value);
                                    }
                                    else
                                    {
                                        o.Value(kv.Key, (string) kv.Value);
                                    }
                                }
                            });
                    }
                });

            result
                .Should()
                .Be(expected);
        }

        private static string Encode(Dictionary<string, object> item)
        {
            var sb = new StringBuilder().Append('{');
                
            var x = item.Count;
            foreach (var kv in item)
            {
                sb.Append('"').Append(kv.Key).Append('"')
                    .Append(':');

                var dict = kv.Value as Dictionary<string, object>;
                if (dict != null)
                {
                    sb.Append(Encode(dict));
                }
                else if (kv.Value is int)
                {
                    sb.Append(kv.Value);
                }
                else
                {
                    sb.Append('"').Append(kv.Value).Append('"');
                }
                x--;
                if (x != 0)
                {
                    sb.Append(',');
                }
            }

            sb.Append('}');

            return sb.ToString();
        }

        private static Dictionary<string, object> DictionaryGen(Generator<string> stringGen, Generator<int> intGen)
        {
            var dictionary = new Dictionary<string, object>();

            var count = Rand(2, 7);
            for (var i = 0; i < count; i++)
            {
                dictionary[stringGen.Take(1).Single()] = (intGen.Take(1).Single() % 2) == 0
                    ? (object) stringGen.Take(1).Single()
                    : (object) intGen.Take(1).Single();
            }

            return dictionary;
        }

        private static int Rand(int min, int max)
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new RandomNumericSequenceGenerator(min, max));
            return fixture.Create<int>();
        }

        private static bool RandBool()
        {
            return Rand(1, 2) == 1;
        }

    }
}
