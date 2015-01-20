using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    public class MethodDefinition
    {
        
        public HttpMethodType HttpMethod { get; set; }

        public string Name { get; set; }

        public List<ParameterDefinition> Parameters { get; set; }

        public ResponseDefinition Response { get; set; }

    }
}
