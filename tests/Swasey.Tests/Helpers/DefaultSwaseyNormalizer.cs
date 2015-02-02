using System;
using System.Collections.Generic;
using System.Linq;

using Swasey.Model;
using Swasey.Normalization;

namespace Swasey.Tests.Helpers
{
    internal class DefaultSwaseyNormalizer
    {

        private DefaultSwaseyNormalizer(INormalizationContext context)
        {
            NormalizationContext = context;
            NormalizedModelMap = new Dictionary<INormalizationApiModel, ModelDefinition>();
        }

        public INormalizationContext NormalizationContext { get; private set; }

        public Dictionary<INormalizationApiModel, ModelDefinition> NormalizedModelMap { get; private set; }

        public static IServiceDefinition Normalize(INormalizationContext context)
        {
            return new DefaultSwaseyNormalizer(context)
                .DoNormalization();
        }

        private IServiceDefinition DoNormalization()
        {
            ExtractEnums();
            NormalizeModels()
                .ToList()
                .ForEach(x => NormalizedModelMap[x.Item1] = x.Item2);

            var serviceDefinition = new ServiceDefinition();

            serviceDefinition.Models.AddRange(NormalizedModelMap.Values);

            return serviceDefinition;
        }

        private IEnumerable<Tuple<INormalizationApiModel, ModelDefinition>> NormalizeModels()
        {
            return NormalizationContext.Models.Values.Select(model => Tuple.Create(model, model.Normalize()));
        }

    }
}
