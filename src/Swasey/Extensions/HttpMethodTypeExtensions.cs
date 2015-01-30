using System;
using System.Linq;

namespace Swasey
{
    internal static class HttpMethodTypeExtensions
    {

        public static HttpMethodType ParseHttpMethodType(this string This)
        {
            if (This == null) goto ReturnDefault;

            if (This.Equals(HttpMethodType.DELETE.ToString(), StringComparison.InvariantCultureIgnoreCase)) return HttpMethodType.DELETE;
            if (This.Equals(HttpMethodType.POST.ToString(), StringComparison.InvariantCultureIgnoreCase)) return HttpMethodType.POST;
            if (This.Equals(HttpMethodType.PUT.ToString(), StringComparison.InvariantCultureIgnoreCase)) return HttpMethodType.PUT;

        ReturnDefault:
            return HttpMethodType.GET;
        }

    }
}
