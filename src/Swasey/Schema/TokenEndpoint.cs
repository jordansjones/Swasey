using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Swasey.Schema
{
    [DataContract]
    internal class TokenEndpoint : BaseEndpoint
    {
        
        [DataMember(Name = "tokenName")]
        public string TokenName { get; set; }

    }
}
