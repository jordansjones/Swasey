using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Swasey.Schema.Version12.Metadata
{
    [DataContract]
    internal class Authorizations : Dictionary<string, Authorization>
    {

    }
}
