using System;
using System.Linq;

namespace Swasey.Model
{
    public sealed class ServicePath : BasePath
    {

        public ServicePath(string value)
            : base(value, IsValid) {}

        public static bool IsValid(string candidate)
        {
            return !string.IsNullOrWhiteSpace(candidate);
        }

        public static bool TryParse(string candidate, out ServicePath servicePath)
        {
            servicePath = null;

            if (!IsValid(candidate))
            {
                return false;
            }

            servicePath = new ServicePath(candidate);
            return true;
        }

        public static implicit operator string(ServicePath servicePath)
        {
            return servicePath.Value;
        }

    }
}
