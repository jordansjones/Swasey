using System;
using System.Linq;
using System.Runtime.Serialization;

using Swasey.Schema.Version12.Metadata;

namespace Swasey.Schema.Version12
{
    internal abstract class BaseApiDeclaration
    {

        [DataMember(Name = "swaggerVersion")]
        public SwaggerVersion SwaggerVersion { get; set; }

        [DataMember(Name = "apiVersion")]
        public string ApiVersion { get; set; }

        [DataMember(Name = "authorizations")]
        public Authorizations Authorizations { get; set; }

    }
}
