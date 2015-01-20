using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Swasey.Schema.Version12
{
    [DataContract]
    internal class ApiDeclaration : BaseApiDeclaration
    {

        [DataMember(Name = "basePath")]
        public string BasePath { get; set; }

        [DataMember(Name = "resourcePath")]
        public string ResourcePath { get; set; }

    }
}
