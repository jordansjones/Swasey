using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    public class Services
    {

        public Services(string basePath, string ns, string version)
        {
            BasePath = basePath;
            Definitions = new List<ClientDefinition>();
            Namespace = ns;
            Version = version;
        }

        public string BasePath { get; private set; }

        public List<ClientDefinition> Definitions { get; private set; }

        public string Namespace { get; private set; }

        public string Version { get; private set; }


    }
}
