using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Normalization
{
    internal class NormalizationContext
    {

        public NormalizationContext()
        {
            Enums = new List<NormalizationApiModelEnum>();
            Models = new List<NormalizationApiModel>();
            Operations = new List<NormalizationApiOperation>();
        }

        public NormalizationContext(NormalizationContext ctx)
            : this()
        {
            if (ctx.Enums.Any())
            {
                Enums.AddRange(ctx.Enums);
            }

            if (ctx.Models.Any())
            {
                Models.AddRange(ctx.Models);
            }

            if (ctx.Operations.Any())
            {
                Operations.AddRange(ctx.Operations);
            }
        }

        public List<NormalizationApiModelEnum> Enums { get; private set; }

        public List<NormalizationApiModel> Models { get; private set; }

        public List<NormalizationApiOperation> Operations { get; private set; }

    }
}
