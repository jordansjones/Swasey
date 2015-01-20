using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    internal class Services : BaseDefinition
    {

        public Services(IModelMetadata meta) 
            : base(meta)
        {
            Definitions = new List<ServiceDefinition>();
        }

        public List<ServiceDefinition> Definitions { get; private set; }


    }
}
