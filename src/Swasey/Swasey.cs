using System;
using System.Linq;
using System.Threading.Tasks;

using Swasey.Commands;
using Swasey.Lifecycle;

namespace Swasey
{
    public class Swasey
    {

        public const SwaggerVersion DefaultSwaggerVersion = SwaggerVersion.Version12;

        public Swasey(GeneratorOptions opts)
        {
            Options = opts;
        }

        public GeneratorOptions Options { get; private set; }

        public async Task Generate(Uri resourceListingUri)
        {
            var lifecycle = new GenerationLifecycle()
                .Enqueue(new LoadResourceListingJsonCommand())
                .Enqueue(new ValidateSwaggerResourceListingCommand())
                .Enqueue(new LoadApiJsonCommand())
                .Enqueue(new ValidateSwaggerApiDefinitionCommand())
                .Enqueue(new ExtractApiModelsCommand())
                .Enqueue(new ExtractApiOperationsCommand())
                .Enqueue(new NormalizeEnumsCommand())
                .Enqueue(new NormalizeModelsCommand())
                .Enqueue(new NormalizeOperationsCommand())
                .Enqueue(new GenerateApiSourcesCommand())
                .Enqueue(new GenerateModelSourcesCommand())
                ;

            await lifecycle.Execute(
                new LifecycleContext(Options)
                {
                    ResourceListingUri = resourceListingUri
                });
        }

    }
}
