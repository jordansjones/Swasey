using System;
using System.Linq;

namespace Swasey
{
    public class GeneratorOptions
    {

        public GeneratorOptions()
        {
            ApiEnumTemplate = ApiModelTemplate = ApiOperationTemplate = string.Empty;
            ApiNamespace = Defaults.DefaultApiNamespace;
            ModelNamespace = Defaults.DefaultModelNamespace;
            Loader = Defaults.DefaultSwaggerJsonLoader;
            OperationWriter = Defaults.DefaultSwaseyOperationWriter;
            EnumWriter = Defaults.DefaultSwaseyEnumWriter;
            ModelWriter = Defaults.DefaultSwaseyModelWriter;
        }

        public string ApiEnumTemplate { get; set; }

        public string ApiModelTemplate { get; set; }

        public string ApiNamespace { get; set; }

        public string ApiOperationTemplate { get; set; }

        public SwaseyEnumWriter EnumWriter { get; set; }

        public SwaggerJsonLoader Loader { get; set; }

        public string ModelNamespace { get; set; }

        public SwaseyModelWriter ModelWriter { get; set; }

        public SwaseyOperationWriter OperationWriter { get; set; }

    }
}
