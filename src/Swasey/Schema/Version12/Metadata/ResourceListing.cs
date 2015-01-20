using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Swasey.Schema.Version12.Metadata
{
    [DataContract]
    internal class ResourceListing : BaseApiDeclaration
    {

        [DataMember(Name = "apis")]
        public ResourceListingApi[] Apis { get; set; }

        [DataMember(Name = "info")]
        public ApiInfo Info { get; set; }
    }
}
