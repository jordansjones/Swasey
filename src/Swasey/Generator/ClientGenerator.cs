using System;
using System.IO;
using System.Linq;

using Swasey.Model;
using Swasey.Templates;

namespace Swasey
{
    public static class ClientGenerator
    {

        public static IServiceDefinition WriteTo(this IServiceDefinition model, TextWriter output)
        {
            DefaultTemplates.ServiceClientClassTemplate(output, model);
            return model;
        }

    }
}