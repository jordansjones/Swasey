using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Normalization
{
    internal class NormalizationContext
    {

        public NormalizationContext()
        {
            Models = new List<NormalizationApiModel>();
            Operations = new List<NormalizationApiOperation>();
        }

        public NormalizationContext(NormalizationContext ctx)
            : this()
        {
            if (ctx.Models.Any())
            {
                Models.AddRange(ctx.Models);
            }

            if (ctx.Operations.Any())
            {
                Operations.AddRange(ctx.Operations);
            }
        }

        public List<NormalizationApiModel> Models { get; private set; }

        public List<NormalizationApiOperation> Operations { get; private set; }

    }
}
