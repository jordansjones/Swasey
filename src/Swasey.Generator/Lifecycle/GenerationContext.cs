using System;
using System.Collections.Generic;
using System.Linq;

using Swasey.Model;

namespace Swasey.Lifecycle
{
    internal class GenerationContext : ILifecycleContext
    {

        private GenerationContext(string apiNamespace, string modelNamespace, SwaggerJsonLoader loader, SwaseyWriter writer)
        {
            ApiNamespace = apiNamespace ?? Defaults.DefaultApiNamespace;
            ModelNamespace = modelNamespace ?? Defaults.DefaultModelNamespace;

            ServiceMetadata = new ServiceMetadata(ApiNamespace, ModelNamespace);

            Loader = loader ?? Defaults.DefaultSwaggerJsonLoader;
            Writer = writer ?? Defaults.DefaultSwaseyWriter;
            ApiPathJsonMapping = new Dictionary<string, dynamic>();
            RawModelDefinitions = new List<IModelDefinition>();
            Models = new Dictionary<QualifiedName, IModelDefinition>();
        }

        public GenerationContext(GeneratorOptions opts)
            : this(opts.ApiNamespace, opts.ModelNamespace, opts.Loader, opts.Writer)
        {
            State = LifecycleState.Continue;
        }

        internal GenerationContext(ILifecycleContext copyFrom)
            : this(copyFrom.ApiNamespace, copyFrom.ModelNamespace, copyFrom.Loader, copyFrom.Writer)
        {
            State = copyFrom.State;
            ResourceListingUri = copyFrom.ResourceListingUri;

            SwaggerVersion = copyFrom.SwaggerVersion;
            ResourceListingJson = copyFrom.ResourceListingJson;

            copyFrom.ApiPathJsonMapping.ToList().ForEach(x => ApiPathJsonMapping.Add(x.Key, x.Value));

            RawModelDefinitions.AddRange(copyFrom.RawModelDefinitions);
            copyFrom.Models.ToList().ForEach(x => Models.Add(x.Key, x.Value));
        }

        public Uri ResourceListingUri { get; internal set; }

        public IServiceMetadata ServiceMetadata { get; internal set; }

        public string ApiNamespace { get; private set; }

        public string ModelNamespace { get; private set; }

        public string SwaggerVersion { get; internal set; }

        public SwaggerJsonLoader Loader { get; private set; }

        public SwaseyWriter Writer { get; private set; }

        public LifecycleState State { get; internal set; }

        public dynamic ResourceListingJson { get; internal set; }

        public Dictionary<string, dynamic> ApiPathJsonMapping { get; private set; }

        IReadOnlyCollection<KeyValuePair<string, dynamic>> ILifecycleContext.ApiPathJsonMapping { get { return ApiPathJsonMapping; } }

        
        public List<IModelDefinition> RawModelDefinitions { get; private set; }
        IReadOnlyList<IModelDefinition> ILifecycleContext.RawModelDefinitions { get { return RawModelDefinitions; } }

        public Dictionary<QualifiedName, IModelDefinition> Models { get; private set; }

        IReadOnlyCollection<KeyValuePair<QualifiedName, IModelDefinition>> ILifecycleContext.Models { get { return Models; } }

    }
}
