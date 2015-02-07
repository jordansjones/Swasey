using System;
using System.Linq;

namespace Swasey
{
    public class GeneratorOptions
    {

        public GeneratorOptions()
        {
            ApiNamespace = Defaults.DefaultApiNamespace;
            ModelNamespace = Defaults.DefaultModelNamespace;
            Loader = Defaults.DefaultSwaggerJsonLoader;
            Writer = Defaults.DefaultSwaseyWriter;
        }

        public string ApiNamespace { get; set; }

        public string ModelNamespace { get; set; }

        public SwaggerJsonLoader Loader { get; set; }

        public SwaseyWriter Writer { get; set; }

    }
}
