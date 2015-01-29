using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Normalization
{
    internal class NormalizationApiModel : BaseNormalizationEntity, INormalizationApiModel
    {

        private readonly List<NormalizationApiModelProperty> _properties = new List<NormalizationApiModelProperty>();

        private readonly List<NormalizationApiModel> _subTypes = new List<NormalizationApiModel>();

        public NormalizationApiModel() {}

        public NormalizationApiModel(INormalizationApiModel copyFrom) : base(copyFrom)
        {
            Description = copyFrom.Description;
            Discriminator = copyFrom.Discriminator;
            Name = copyFrom.Name;

            Properties.AddRange(
                (copyFrom.Properties ?? new Dictionary<string, INormalizationApiModelProperty>())
                    .Select(x => new NormalizationApiModelProperty(x.Value))
                );

            SubTypes.AddRange(
                (copyFrom.SubTypes ?? new List<INormalizationApiModel>())
                    .Select(x => new NormalizationApiModel(x))
                );
        }

        public List<NormalizationApiModelProperty> Properties
        {
            get { return _properties; }
        }

        public List<NormalizationApiModel> SubTypes
        {
            get { return _subTypes; }
        }

        public string Description { get; set; }

        public string Discriminator { get; set; }

        public string Name { get; set; }

        IReadOnlyDictionary<string, INormalizationApiModelProperty> INormalizationApiModel.Properties
        {
            get { return Properties.OfType<INormalizationApiModelProperty>().ToDictionary(x => x.Name, x => x); }
        }

        IReadOnlyList<INormalizationApiModel> INormalizationApiModel.SubTypes
        {
            get { return SubTypes.OfType<INormalizationApiModel>().ToList(); }
        }

    }
}
