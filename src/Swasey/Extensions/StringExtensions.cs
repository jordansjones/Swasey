using System;
using System.Linq;

namespace Swasey
{
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

            if (char.IsLower(name[0]))
            {
                name = char.ToUpper(name[0]) + name.Substring(1);
            }

            return name;
        }

    }
}
