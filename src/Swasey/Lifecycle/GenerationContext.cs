using System;
using System.Collections.Generic;
using System.Linq;

using Swasey.Model;
using Swasey.Normalization;

namespace Swasey.Lifecycle
{
    internal class GenerationContext : ILifecycleContext
    {

        private GenerationContext(string apiNamespace, string modelNamespace, SwaggerJsonLoader loader, SwaseyNormalizer normalizer, SwaseyWriter writer)
        {
            ApiNamespace = apiNamespace ?? Defaults.DefaultApiNamespace;
            ModelNamespace = modelNamespace ?? Defaults.DefaultModelNamespace;

            ServiceMetadata = new ServiceMetadata(ApiNamespace, ModelNamespace);

            Loader = loader ?? Defaults.DefaultSwaggerJsonLoader;
            Normalizer = normalizer ?? Defaults.DefaultSwaseyNormalizer;
            Writer = writer ?? Defaults.DefaultSwaseyWriter;

            ApiPathJsonMapping = new Dictionary<string, dynamic>();

            NormalizationContext = new NormalizationContext();
        }

        public GenerationContext(GeneratorOptions opts)
            : this(opts.ApiNamespace, opts.ModelNamespace, opts.Loader, opts.Normalizer, opts.Writer)
        {
            State = LifecycleState.Continue;
        }

        internal GenerationContext(ILifecycleContext copyFrom)
            : this(copyFrom.ApiNamespace, copyFrom.ModelNamespace, copyFrom.Loader, copyFrom.Normalizer, copyFrom.Writer)
        {
            State = copyFrom.State;
            ResourceListingUri = copyFrom.ResourceListingUri;

            SwaggerVersion = copyFrom.SwaggerVersion;
            ResourceListingJson = copyFrom.ResourceListingJson;

            NormalizationContext = new NormalizationContext(copyFrom.NormalizationContext);

            ServiceDefinition = copyFrom.ServiceDefinition;

            copyFrom.ApiPathJsonMapping.ToList().ForEach(x => ApiPathJsonMapping.Add(x.Key, x.Value));
        }

        public Uri ResourceListingUri { get; internal set; }

        public IServiceMetadata ServiceMetadata { get; internal set; }

        public string ApiNamespace { get; private set; }

        public string ModelNamespace { get; private set; }

        public string SwaggerVersion { get; internal set; }

        public SwaggerJsonLoader Loader { get; private set; }

        public SwaseyNormalizer Normalizer { get; private set; }

        public SwaseyWriter Writer { get; private set; }

        public LifecycleState State { get; internal set; }

        public dynamic ResourceListingJson { get; internal set; }

        public Dictionary<string, dynamic> ApiPathJsonMapping { get; private set; }

        IReadOnlyCollection<KeyValuePair<string, dynamic>> ILifecycleContext.ApiPathJsonMapping { get { return ApiPathJsonMapping; } }

        public NormalizationContext NormalizationContext { get; private set; }
        INormalizationContext ILifecycleContext.NormalizationContext { get { return NormalizationContext; } }

        public IServiceDefinition ServiceDefinition { get; set; }

    }
}
