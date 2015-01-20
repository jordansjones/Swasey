using System;
using System.Linq;

namespace Swasey.Tests.Helpers
{
    internal static class SwaggerVersionExtensions
    {

        public static string Version(this SwaggerVersion This)
        {
            switch (This)
            {
                case SwaggerVersion.Version10:
                    return "1.0";
                case SwaggerVersion.Version11:
                    return "1.1";
                case SwaggerVersion.Version12:
                    return "1.2";
                case SwaggerVersion.Version20:
                    return "2.0";
                default:
                    throw new ArgumentOutOfRangeException(string.Format("I don't know what version this is: {0}", This));
            }
        }

    }
}
