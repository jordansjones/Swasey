using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Swasey.Schema
{
    [DataContract]
    internal class Authorizations : Dictionary<string, Authorization>
    {

    }
}
