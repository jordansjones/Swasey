using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Normalization
{
    internal class NormalizationApiModelEnum : BaseNormalizationEntity
    {

        private readonly List<string> _values = new List<string>();

        public string Description { get; set; }

        public string Name { get; set; }

        public string ResourceName { get; set; }

        public string ResourcePath { get; set; }

        public List<string> Values
        {
            get { return _values; }
        }

    }
}
