using System;
using System.Linq;

namespace Swasey.Tests.Helpers
{
    internal static class Ensure
    {

        public static string NotNullOrEmpty(string val, string argumentName = null)
        {
            if (string.IsNullOrWhiteSpace(val))
            {
                var msg = "Failed NullOrEmpty Check";
                if (!string.IsNullOrWhiteSpace(argumentName))
                {
                    msg += string.Format(" [Arugment: {0}]", argumentName);
                }
                throw new ArgumentNullException(msg);
            }
            return val;
        }

    }
}
