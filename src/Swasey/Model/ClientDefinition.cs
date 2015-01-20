using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    public class ClientDefinition
    {

        public string Name { get; set; }
        public string Path { get; set; }

        public List<MethodDefinition> Operations { get; set; }

    }
}
