using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Swasey.Schema
{
    [DataContract]
    internal class ApiInfo
    {

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "termsOfServiceUrl")]
        public string TermsOfServiceUrl { get; set; }

        [DataMember(Name = "contact")]
        public string Contact { get; set; }

        [DataMember(Name = "license")]
        public string License { get; set; }

        [DataMember(Name = "licenseUrl")]
        public string LicenseUrl { get; set; }

    }
}
