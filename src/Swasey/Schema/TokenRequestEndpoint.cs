﻿using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Swasey.Schema
{
    [DataContract]
    internal class TokenRequestEndpoint : BaseEndpoint
    {
        
        [DataMember(Name = "clientIdName")]
        public string ClientIdName { get; set; }
        
        [DataMember(Name = "clientSecretName")]
        public string ClientSecretName { get; set; }

    }
}
