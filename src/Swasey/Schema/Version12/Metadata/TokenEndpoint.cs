using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Swasey.Schema.Version12.Metadata
{
    [DataContract]
    internal class TokenEndpoint : BaseEndpoint
    {
        
        [DataMember(Name = "tokenName")]
        public string TokenName { get; set; }

    }
}
