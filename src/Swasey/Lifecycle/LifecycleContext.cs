using System;
using System.Collections.Generic;
using System.Linq;

using Swasey.Model;
using Swasey.Normalization;

namespace Swasey.Lifecycle
{
    internal class LifecycleContext : ILifecycleContext
    {

        private LifecycleContext(string apiNamespace, string modelNamespace, SwaggerJsonLoader loader, SwaseyWriter writer)
        {
            ApiNamespace = apiNamespace ?? Defaults.DefaultApiNamespace;
            ModelNamespace = modelNamespace ?? Defaults.DefaultModelNamespace;

            ServiceMetadata = new ServiceMetadata(ApiNamespace, ModelNamespace);

            ServiceDefinition = new ServiceDefinition();

            Loader = loader ?? Defaults.DefaultSwaggerJsonLoader;
            Writer = writer ?? Defaults.DefaultSwaseyWriter;

            ApiPathJsonMapping = new Dictionary<string, dynamic>();

            NormalizationContext = new NormalizationContext();
        }

        public LifecycleContext(GeneratorOptions opts)
            : this(opts.ApiNamespace, opts.ModelNamespace, opts.Loader, opts.Writer)
        {
            State = LifecycleState.Continue;
        }

        internal LifecycleContext(ILifecycleContext copyFrom)
            : this(copyFrom.ApiNamespace, copyFrom.ModelNamespace, copyFrom.Loader, copyFrom.Writer)
        {
            State = copyFrom.State;
            ResourceListingUri = copyFrom.ResourceListingUri;

            SwaggerVersion = copyFrom.SwaggerVersion;
            ResourceListingJson = copyFrom.ResourceListingJson;

            NormalizationContext = new NormalizationContext(copyFrom.NormalizationContext);

            ServiceDefinition = new ServiceDefinition(copyFrom.ServiceDefinition);

            copyFrom.ApiPathJsonMapping.ToList().ForEach(x => ApiPathJsonMapping.Add(x.Key, x.Value));
        }

        public Dictionary<string, dynamic> ApiPathJsonMapping { get; private set; }

        public NormalizationContext NormalizationContext { get; private set; }

        public ServiceDefinition ServiceDefinition { get; internal set; }

        public string ApiNamespace { get; private set; }

        IReadOnlyCollection<KeyValuePair<string, dynamic>> ILifecycleContext.ApiPathJsonMapping
        {
            get { return ApiPathJsonMapping; }
        }

        public SwaggerJsonLoader Loader { get; private set; }

        public string ModelNamespace { get; private set; }

        INormalizationContext ILifecycleContext.NormalizationContext
        {
            get { return NormalizationContext; }
        }

        public dynamic ResourceListingJson { get; internal set; }

        public Uri ResourceListingUri { get; internal set; }

        IServiceDefinition ILifecycleContext.ServiceDefinition
        {
            get { return ServiceDefinition; }
        }

        public IServiceMetadata ServiceMetadata { get; internal set; }

        public LifecycleState State { get; internal set; }

        public string SwaggerVersion { get; internal set; }

        public SwaseyWriter Writer { get; private set; }

    }
}
