using System.Runtime.Serialization;

namespace Swasey.Schema.Version12
{
    [DataContract]
    internal class DtoModel
    {

        [DataMember(Name = "id")]
        public string Name { get; private set; }

        [DataMember(Name = "description")]
        public string Descrption { get; set; }

        [DataMember(Name = "required")]
        public string[] RequiredProperties { get; set; }
         

    }
}