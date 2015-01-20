using System;
using System.IO;
using System.Linq;

using Swasey.Model;
using Swasey.Templates;

namespace Swasey
{
    public class ClientGenerator
    {

        public static string GenerateClient(Services servicesModel)
        {
            var hbs = Handlebars.Handlebars.Create();
            var fileHeader = hbs.Compile(new StringReader(TemplateFactory.FileHeader));
            hbs.RegisterTemplate("FileHeader", fileHeader);
            using (var sw = new StringWriter())
            {
                hbs.Compile(new StringReader(TemplateFactory.ServiceClientClass))(sw, servicesModel);
                return sw.GetStringBuilder().ToString();
            }
        }

    }
}