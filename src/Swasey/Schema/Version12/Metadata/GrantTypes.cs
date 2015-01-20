using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Swasey.Schema.Version12.Metadata
{
    [DataContract]
    internal class GrantTypes
    {
        
        [DataMember(Name = "implicit")]
        public ImplicitGrantType Implicit { get; set; }
        
        [DataMember(Name = "authorization_code")]
        public AuthorizationCodeGrantType AuthorizationCode { get; set; }

    }
    
    [DataContract]
    internal class ImplicitGrantType
    {
        
        [DataMember(Name = "tokenName")]
        public string TokenName { get; set; }
        
        [DataMember(Name = "loginEndpoint")]
        public LoginEndpoint LoginEndpoint { get; set; }

    }
    
    [DataContract]
    internal class AuthorizationCodeGrantType
    {
        
        [DataMember(Name = "tokenRequestEndpoint")]
        public TokenRequestEndpoint TokenRequestEndpoint { get; set; }
        
        [DataMember(Name = "tokenEndpoint")]
        public TokenEndpoint TokenEndpoint { get; set; }

    }
}