using System;
using System.Linq;

using Swasey.Schema;

namespace Swasey
{
    internal static class BaseEndpointExtensions
    {

        public static Uri UrlAsUri<T>(this T This)
            where T : BaseEndpoint
        {
            if (This == null || string.IsNullOrWhiteSpace(This.Url)) return null;

            return new Uri(This.Url);
        }

    }
}
