using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Swasey.Schema.Version12.Metadata
{
    [DataContract]
    internal class Authorization
    {
        
        [DataMember(Name = "type")]
        public string AuthorizationType { get; set; }
        
        [DataMember(Name = "passAs")]
        public string PassAs { get; set; }
        
        [DataMember(Name = "keyname")]
        public string KeyName { get; set; }
        
        [DataMember(Name = "scopes")]
        public AuthorizationScope[] AuthorizationScopes { get; set; }
        
        [DataMember(Name = "grantTypes")]
        public GrantTypes GrantTypes { get; set; }
    }
}
