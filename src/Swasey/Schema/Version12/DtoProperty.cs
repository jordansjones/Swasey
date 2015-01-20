using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Swasey.Schema.Version12
{

    [DataContract]
    internal class DtoProperty
    {

        [DataMember(Name = "$ref")]
        public string Ref { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        public string Format { get; set; }

        public string DefaultValue { get; set; }

        public string Enum { get; set; }

        public string Minimum { get; set; }

        public string Maximum { get; set; }
    }

    [DataContract]
    internal class DtoProperties : Dictionary<string, dynamic>
    {}
}
