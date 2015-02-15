using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Swasey.Model;

namespace Swasey
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class StringExtensions
    {

        public static string ResourceNameFromPath(this string resourcePath)
        {
            if (string.IsNullOrWhiteSpace(resourcePath)) throw new ArgumentNullException("resourcePath");

            var name = resourcePath;

            if (name.StartsWith("/"))
            {
                name = name.Substring(1);
            }

            return name.UCFirst();
        }

        public static string UCFirst(this string This)
        {
            if (string.IsNullOrWhiteSpace(This)) return This;
            if (This.Length == 1) return This.ToUpperInvariant();

            return char.ToUpperInvariant(This[0]) + This.Substring(1);
        }

        public static string LCFirst(this string This)
        {
            if (string.IsNullOrWhiteSpace(This)) return This;
            if (This.Length == 1) return This.ToLowerInvariant();

            return char.ToLowerInvariant(This[0]) + This.Substring(1);
        }

        public static string UCFirst(this QualifiedName This)
        {
            return This.ToString().UCFirst();
        }

        public static string LCFirst(this QualifiedName This)
        {
            return This.ToString().LCFirst();
        }

        public static string UCFirst(this ParameterName This)
        {
            return This.ToString().UCFirst();
        }

        public static string LCFirst(this ParameterName This)
        {
            return This.ToString().LCFirst();
        }

    }
}
