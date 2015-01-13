using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Swasey.Schema
{
    internal abstract class BaseEndpoint
    {

        [DataMember(Name = "url")]
        public string Url { get; set; }

    }
}
