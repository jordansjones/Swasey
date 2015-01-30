using System;
using System.Linq;
using System.Threading.Tasks;

using Swasey.Model;
using Swasey.Normalization;

namespace Swasey
{

    public delegate Task<string> SwaggerJsonLoader(Uri uri);

    public delegate void SwaseyWriter(WriteType type, string name, string content);

    public delegate IServiceDefinition SwaseyNormalizer(INormalizationContext normalizationContext);

    public enum WriteType
    {

        Api,

        Model

    }


    internal static class Defaults
    {

        public const string DefaultApiNamespace = "Service.Client.Api";

        public const string DefaultModelNamespace = "Service.Client.Model";

        public static Task<string> DefaultSwaggerJsonLoader(Uri uri)
        {
            throw new NotImplementedException();
        }

        public static void DefaultSwaseyWriter(WriteType type, string name, string content)
        {
            throw new NotImplementedException();
        }

        public static IServiceDefinition DefaultSwaseyNormalizer(INormalizationContext normalizationContext)
        {
            throw new NotImplementedException();
        }

    }
}
