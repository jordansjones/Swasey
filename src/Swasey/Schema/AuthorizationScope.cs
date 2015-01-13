using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Swasey.Schema
{
    [DataContract]
    internal class AuthorizationScope
    {
        
        [DataMember(Name = "scope")]
        public string Name { get; set; }
        
        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}
