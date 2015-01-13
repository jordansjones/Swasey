using System.Runtime.Serialization;

namespace Swasey.Schema
{
    [DataContract]
    internal class GrantTypes
    {
        
        [DataMember(Name = "implicit")]
        public ImplicitGrantType Implicity { get; set; }
        
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