using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Normalization
{
    internal class NormalizationContext : INormalizationContext
    {

        public NormalizationContext()
        {
            Models = new List<INormalizationApiModel>();
            Operations = new List<INormalizationApiOperation>();
        }

        public NormalizationContext(INormalizationContext ctx)
            : this()
        {
            if (ctx.Models.Any()) { Models.AddRange(ctx.Models.Values); }
            if (ctx.Operations.Any()) { Operations.AddRange(ctx.Operations.Values); }
        }

        public List<INormalizationApiModel> Models { get; private set; }

        public List<INormalizationApiOperation> Operations { get; private set; }

        #region INormalizationContext Implementation

        IReadOnlyDictionary<string, INormalizationApiModel> INormalizationContext.Models
        {
            get { return Models.ToDictionary(x => x.Name, x => x); }
        }

        IReadOnlyDictionary<string, INormalizationApiOperation> INormalizationContext.Operations
        {
            get { return Operations.ToDictionary(x => x.Name, x => x); }
        }

        #endregion
    }
}
