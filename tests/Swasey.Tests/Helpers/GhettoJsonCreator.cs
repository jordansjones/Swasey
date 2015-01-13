using System;
using System.Linq;
using System.Text;

namespace Swasey.Tests.Helpers
{
    internal static class GhettoJsonCreator
    {

        public static string Object(Action<IJsonObjectBuilder> factory)
        {
            var builder = new GhettoJsonBuilderImpl();
            builder.AddObject(factory);
            builder.TryTruncateTrailingComma();
            return builder.Sb.ToString();
        }

        public static string Array(Action<IJsonArrayBuilder> factory)
        {
            var builder = new GhettoJsonBuilderImpl();
            builder.AddArray(factory);
            builder.TryTruncateTrailingComma();
            return builder.Sb.ToString();
        }

        public static string Value(string value)
        {
            var builder = new GhettoJsonBuilderImpl();
            builder.AddValue(value);
            builder.TryTruncateTrailingComma();
            return builder.Sb.ToString();
        }

        private class GhettoJsonBuilderImpl : IJsonObjectBuilder, IJsonArrayBuilder
        {

            public GhettoJsonBuilderImpl()
                : this(new StringBuilder())
            {}

            private GhettoJsonBuilderImpl(StringBuilder sb)
            {
                Sb = sb;
            }

            public StringBuilder Sb { get; private set; }

            public IJsonObjectBuilder Array(string name, Action<IJsonArrayBuilder> factory)
            {
                Quote(name).Append(":");
                AddArray(factory);
                return this;
            }

            public IJsonObjectBuilder Object(string name, Action<IJsonObjectBuilder> factory)
            {
                Quote(name).Append(":");
                AddObject(factory);
                return this;
            }

            public IJsonObjectBuilder Value(string name, string value)
            {
                Quote(name).Append(":");
                AddValue(value);
                return this;
            }

            public IJsonObjectBuilder Value(string name, int value)
            {
                Quote(name).Append(":");
                AddValue(value);
                return this;
            }

            IJsonArrayBuilder IJsonArrayBuilder.Array(Action<IJsonArrayBuilder> factory)
            {
                AddArray(factory);
                return this;
            }

            IJsonArrayBuilder IJsonArrayBuilder.Object(Action<IJsonObjectBuilder> factory)
            {
                AddObject(factory);
                return this;
            }

            IJsonArrayBuilder IJsonArrayBuilder.Value(string value)
            {
                AddValue(value);
                return this;
            }

            IJsonArrayBuilder IJsonArrayBuilder.Value(int value)
            {
                AddValue(value);
                return this;
            }

            public void AddArray(Action<GhettoJsonBuilderImpl> factory)
            {
                Sb.Append("[");

                var builder = new GhettoJsonBuilderImpl();
                factory(builder);
                builder.TryTruncateTrailingComma();

                Sb.Append(builder.Sb);

                Sb.Append("]");
                Sb.Append(",");
            }

            public void AddObject(Action<GhettoJsonBuilderImpl> factory)
            {
                Sb.Append("{");

                var builder = new GhettoJsonBuilderImpl();
                factory(builder);
                builder.TryTruncateTrailingComma();

                Sb.Append(builder.Sb);

                Sb.Append("}");
                Sb.Append(",");
            }

            public void AddValue(int value)
            {
                Sb.Append(value);
                Sb.Append(",");
            }

            public void AddValue(string value)
            {
                Quote(value);
                Sb.Append(",");
            }

            internal StringBuilder Quote(string value)
            {
                return Sb.AppendEncodedString(value);
            }

            internal void TryTruncateTrailingComma()
            {
                if (Sb.Length == 0) return;

                var offset = Sb.Length - 1;
                if (Sb[offset] == ',')
                {
                    Sb.Remove(offset, 1);
                }
            }

        }

    }

    internal static class JsonStringExtensions
    {

        public static unsafe StringBuilder AppendEncodedString(this StringBuilder This, string val)
        {
            if (val == null) return This;
            This.Append('"');

            fixed (char* sf = val)
            {
                char* str = sf;
                char c;
                var len = val.Length;

                while (len > 0)
                {
                    c = *str;
                    str++;
                    len--;

                    if (c == '\\')
                    {
                        This.Append(@"\\");
                        continue;
                    }

                    if (c == '"')
                    {
                        This.Append("\\\"");
                        continue;
                    }

                    switch (c)
                    {
                        case '\u0000': This.Append(@"\u0000"); continue;
                        case '\u0001': This.Append(@"\u0001"); continue;
                        case '\u0002': This.Append(@"\u0002"); continue;
                        case '\u0003': This.Append(@"\u0003"); continue;
                        case '\u0004': This.Append(@"\u0004"); continue;
                        case '\u0005': This.Append(@"\u0005"); continue;
                        case '\u0006': This.Append(@"\u0006"); continue;
                        case '\u0007': This.Append(@"\u0007"); continue;
                        case '\u0008': This.Append(@"\b"); continue;
                        case '\u0009': This.Append(@"\t"); continue;
                        case '\u000A': This.Append(@"\n"); continue;
                        case '\u000B': This.Append(@"\u000B"); continue;
                        case '\u000C': This.Append(@"\f"); continue;
                        case '\u000D': This.Append(@"\r"); continue;
                        case '\u000E': This.Append(@"\u000E"); continue;
                        case '\u000F': This.Append(@"\u000F"); continue;
                        case '\u0010': This.Append(@"\u0010"); continue;
                        case '\u0011': This.Append(@"\u0011"); continue;
                        case '\u0012': This.Append(@"\u0012"); continue;
                        case '\u0013': This.Append(@"\u0013"); continue;
                        case '\u0014': This.Append(@"\u0014"); continue;
                        case '\u0015': This.Append(@"\u0015"); continue;
                        case '\u0016': This.Append(@"\u0016"); continue;
                        case '\u0017': This.Append(@"\u0017"); continue;
                        case '\u0018': This.Append(@"\u0018"); continue;
                        case '\u0019': This.Append(@"\u0019"); continue;
                        case '\u001A': This.Append(@"\u001A"); continue;
                        case '\u001B': This.Append(@"\u001B"); continue;
                        case '\u001C': This.Append(@"\u001C"); continue;
                        case '\u001D': This.Append(@"\u001D"); continue;
                        case '\u001E': This.Append(@"\u001E"); continue;
                        case '\u001F': This.Append(@"\u001F"); continue;
                        default: This.Append(c); continue;
                    }
                }
            }

            This.Append('"');
            return This;
        }

    }

    internal interface IJsonObjectBuilder
    {

        IJsonObjectBuilder Array(string name, Action<IJsonArrayBuilder> factory);
        IJsonObjectBuilder Object(string name, Action<IJsonObjectBuilder> factory);
        IJsonObjectBuilder Value(string name, string value);
        IJsonObjectBuilder Value(string name, int value);

    }

    internal interface IJsonArrayBuilder
    {


        IJsonArrayBuilder Array(Action<IJsonArrayBuilder> factory);
        IJsonArrayBuilder Object(Action<IJsonObjectBuilder> factory);
        IJsonArrayBuilder Value(string value);
        IJsonArrayBuilder Value(int value);


    }
}
