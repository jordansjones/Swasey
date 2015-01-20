using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Swasey.Schema.Version12.Metadata
{
    [DataContract]
    internal class ResourceListingApi
    {
        
        [DataMember(Name = "path")]
        public string Path { get; set; }
        
        [DataMember(Name = "description")]
        public string Description { get; set; }

    }
}
