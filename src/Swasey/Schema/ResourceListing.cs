using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Swasey.Schema
{
    [DataContract]
    internal class ResourceListing
    {

        [DataMember(Name = "swaggerVersion")]
        public string SwaggerVersion { get; set; }

        [DataMember(Name = "apis")]
        public ResourceListingApi[] Apis { get; set; }

        [DataMember(Name = "apiVersion")]
        public string ApiVersion { get; set; }

        [DataMember(Name = "info")]
        public ApiInfo ApiInfo { get; set; }

        [DataMember(Name = "authorizations")]
        public Authorizations Authorizations { get; set; }
    }
}
