using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Swasey.Model;
using Swasey.Normalization;

namespace Swasey.Lifecycle
{
    internal class LifecycleContext : ILifecycleContext
    {

        private LifecycleContext(string apiNamespace, string modelNamespace, SwaggerJsonLoader loader, SwaseyOperationWriter operationWriter, SwaseyEnumWriter enumWriter, SwaseyModelWriter modelWriter)
        {
            ApiNamespace = apiNamespace ?? Defaults.DefaultApiNamespace;
            ModelNamespace = modelNamespace ?? Defaults.DefaultModelNamespace;

            ServiceMetadata = new ServiceMetadata(ApiNamespace, ModelNamespace);

            ServiceDefinition = new ServiceDefinition();

            Loader = loader ?? Defaults.DefaultSwaggerJsonLoader;
            OperationWriter = operationWriter ?? Defaults.DefaultSwaseyOperationWriter;
            EnumWriter = enumWriter ?? Defaults.DefaultSwaseyEnumWriter;
            ModelWriter = modelWriter ?? Defaults.DefaultSwaseyModelWriter;

            ApiPathJsonMapping = new Dictionary<string, dynamic>();

            NormalizationContext = new NormalizationContext();
        }

        public LifecycleContext(GeneratorOptions opts)
            : this(opts.ApiNamespace, opts.ModelNamespace, opts.Loader, opts.OperationWriter, opts.EnumWriter, opts.ModelWriter)
        {
            State = LifecycleState.Continue;

            ApiEnumTemplate = SwaseyEngine.Compile(opts.ApiEnumTemplate);
            ApiModelTemplate = SwaseyEngine.Compile(opts.ApiModelTemplate);
            ApiOperationTemplate = SwaseyEngine.Compile(opts.ApiOperationTemplate);

            OperationFilter = opts.OperationFilter ?? Defaults.DefaultOperationFilter;
            OperationParameterFilter = opts.OperationParameterFilter ?? Defaults.DefaultOperationParameterFilter;
        }

        internal LifecycleContext(ILifecycleContext copyFrom)
            : this(copyFrom.ApiNamespace, copyFrom.ModelNamespace, copyFrom.Loader, copyFrom.OperationWriter, copyFrom.EnumWriter, copyFrom.ModelWriter)
        {
            State = copyFrom.State;
            ResourceListingUri = copyFrom.ResourceListingUri;

            ApiEnumTemplate = copyFrom.ApiEnumTemplate;
            ApiModelTemplate = copyFrom.ApiModelTemplate;
            ApiOperationTemplate = copyFrom.ApiOperationTemplate;

            SwaggerVersion = copyFrom.SwaggerVersion;
            ResourceListingJson = copyFrom.ResourceListingJson;

            OperationFilter = copyFrom.OperationFilter ?? Defaults.DefaultOperationFilter;
            OperationParameterFilter = copyFrom.OperationParameterFilter ?? Defaults.DefaultOperationParameterFilter;

            NormalizationContext = new NormalizationContext(copyFrom.NormalizationContext);

            ServiceDefinition = new ServiceDefinition(copyFrom.ServiceDefinition);

            copyFrom.ApiPathJsonMapping.ToList().ForEach(x => ApiPathJsonMapping.Add(x.Key, x.Value));
        }

        //don't need this anymore: it maps each 'path' to its operations (I think). 
        //That is done for us in Swagger 2.0.
        public Dictionary<string, dynamic> ApiPathJsonMapping { get; private set; }

        //this should work instead.
        public dynamic Paths { get; set; }

        public ServiceDefinition ServiceDefinition { get; internal set; }

        public Action<TextWriter, object> ApiEnumTemplate { get; private set; }

        public Action<TextWriter, object> ApiModelTemplate { get; private set; }

        public string ApiNamespace { get; private set; }

        public Action<TextWriter, object> ApiOperationTemplate { get; private set; }

        IReadOnlyCollection<KeyValuePair<string, dynamic>> ILifecycleContext.ApiPathJsonMapping
        {
            get { return ApiPathJsonMapping; }
        }

        public SwaseyEnumWriter EnumWriter { get; private set; }

        public SwaggerJsonLoader Loader { get; private set; }

        public string ModelNamespace { get; private set; }

        public SwaseyModelWriter ModelWriter { get; private set; }

        public NormalizationContext NormalizationContext { get; private set; }

        public Func<dynamic, bool> OperationFilter { get; private set; }

        public Func<dynamic, bool> OperationParameterFilter { get; private set; }

        public SwaseyOperationWriter OperationWriter { get; private set; }

        public dynamic ResourceListingJson { get; internal set; }

        public Uri ResourceListingUri { get; internal set; }

        IServiceDefinition ILifecycleContext.ServiceDefinition
        {
            get { return ServiceDefinition; }
        }

        public IServiceMetadata ServiceMetadata { get; internal set; }

        public LifecycleState State { get; internal set; }

        public string SwaggerVersion { get; internal set; }
    }
}
