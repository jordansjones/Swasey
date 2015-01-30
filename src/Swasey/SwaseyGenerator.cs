using System;
using System.Linq;
using System.Threading.Tasks;

using Swasey.Commands;
using Swasey.Lifecycle;

namespace Swasey
{
    public class SwaseyGenerator
    {

        public SwaseyGenerator(GeneratorOptions opts)
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
                .Enqueue(new SwaseyNormalizerCommand())
                .Enqueue(new GenerateApiSourcesCommand())
                .Enqueue(new GenerateModelSourcesCommand())
                ;

            await lifecycle.Execute(new GenerationContext(Options)
            {
                ResourceListingUri = resourceListingUri
            });
        }

    }
}
