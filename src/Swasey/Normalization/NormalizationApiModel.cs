﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Normalization
{
    internal class NormalizationApiModel : BaseNormalizationEntity
    {

        private readonly List<NormalizationApiModelProperty> _properties = new List<NormalizationApiModelProperty>();

        private readonly List<NormalizationApiModel> _subTypes = new List<NormalizationApiModel>();

        private readonly List<string> _rawSubTypes = new List<string>();

        public NormalizationApiModel() {}

        public NormalizationApiModel(NormalizationApiModel copyFrom) : base(copyFrom)
        {
            if (copyFrom == null) return;

            Description = copyFrom.Description;
            Discriminator = copyFrom.Discriminator;
            Name = copyFrom.Name;
            ResourceName = copyFrom.ResourceName;
            ResourcePath = copyFrom.ResourcePath;

            Properties.AddRange(copyFrom.Properties);

            SubTypes.AddRange(copyFrom.SubTypes);
        }

        public List<NormalizationApiModelProperty> Properties
        {
            get { return _properties; }
        }

        public string ResourceName { get; set; }

        public string ResourcePath { get; set; }

        public List<NormalizationApiModel> SubTypes
        {
            get { return _subTypes; }
        }

        public List<string> RawSubTypes { get { return _rawSubTypes; } }

        public string Description { get; set; }

        public string Discriminator { get; set; }

        public string Name { get; set; }

    }
}
