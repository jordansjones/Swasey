using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Normalization
{
    internal class NormalizationContext : INormalizationContext
    {

        public NormalizationContext()
        {
            Models = new List<NormalizationApiModel>();
            Operations = new List<NormalizationApiOperation>();
        }

        public NormalizationContext(INormalizationContext ctx)
            : this()
        {
            Models.AddRange(
                (ctx.Models ?? new Dictionary<string, INormalizationApiModel>())
                    .Select(x => new NormalizationApiModel(x.Value))
                );

            Operations.AddRange(
                (ctx.Operations ?? Enumerable.Empty<IGrouping<string, INormalizationApiOperation>>())
                    .SelectMany(x => x)
                    .Select(x => new NormalizationApiOperation(x))
                );
        }

        public List<NormalizationApiModel> Models { get; private set; }

        public List<NormalizationApiOperation> Operations { get; private set; }

        #region INormalizationContext Implementation

        IReadOnlyDictionary<string, INormalizationApiModel> INormalizationContext.Models
        {
            get { return Models.OfType<INormalizationApiModel>().ToDictionary(x => x.Name, x => x); }
        }

        ILookup<string, INormalizationApiOperation> INormalizationContext.Operations
        {
            get { return Operations.OfType<INormalizationApiOperation>().ToLookup(x => x.ResourcePath); }
        }

        #endregion
    }
}
