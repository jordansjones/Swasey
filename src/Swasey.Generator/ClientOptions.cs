using System;
using System.Linq;

namespace Swasey
{
    public class ClientOptions
    {

        private const string DefaultBaseNamespace = "Service.Client";

        public ClientOptions()
        {
            BaseNamespace = DefaultBaseNamespace;
            BasePath = string.Empty;
        }

        public string BaseNamespace { get; set; }

        public string BasePath { get; set; }

    }
}
