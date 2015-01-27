using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey
{
    internal static class SwaseyGeneratorExtensions
    {

        public static IEnumerable<string> ParseApiPaths(this SwaseyGenerator This, object apiCollection)
        {
            var collection = (dynamic) apiCollection;
            foreach (var api in collection)
            {
                yield return (string) api.path;
            }
        }

    }
}
