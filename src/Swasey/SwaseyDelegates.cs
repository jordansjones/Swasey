using System;
using System.Linq;
using System.Threading.Tasks;

namespace Swasey
{

    public delegate Task<string> SwaggerJsonLoader(Uri uri);

    public delegate void SwaseyWriter(WriteType type, string name, string content);

    public enum WriteType
    {

        Enum,

        Model,

        Operation

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

    }
}
