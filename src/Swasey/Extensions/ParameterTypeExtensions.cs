using System;
using System.Linq;

using Swasey.Model;

namespace Swasey
{
    internal static class ParameterTypeExtensions
    {

        public static ParameterType ParseParameterType(this string This)
        {
            if (This == null) goto ReturnDefault;

            if (This.Equals(ParameterType.Header.ToString(), StringComparison.InvariantCultureIgnoreCase)) return ParameterType.Header;

            if (This.Equals(ParameterType.Form.ToString(), StringComparison.InvariantCultureIgnoreCase)) return ParameterType.Form;

            if (This.Equals(ParameterType.Path.ToString(), StringComparison.InvariantCultureIgnoreCase)) return ParameterType.Path;

            if (This.Equals(ParameterType.Query.ToString(), StringComparison.InvariantCultureIgnoreCase)) return ParameterType.Query;

        ReturnDefault:
            return ParameterType.Body;
        }

    }
}
