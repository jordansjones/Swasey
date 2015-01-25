using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Reflection;

using Jil;

namespace Swasey
{
    public class SwaseyGenerator
    {

        public SwaseyGenerator(ClientOptions opts)
        {
            Options = opts;
        }

        public ClientOptions Options { get; private set; }

        public async Task Generate(string resourceListing, Func<string, Task<string>> apiSupplier)
        {
            dynamic resources = JSON.DeserializeDynamic(resourceListing);
            if (resources == null || !resources.ContainsKey("swaggerVersion"))
            {
                throw new Exception("Invalid Swagger spec");
            }
            if (!resources.ContainsKey("apis"))
            {
                throw new Exception("No 'apis' specified");
            }

            var apis = resources.apis;
            if (apis == null || apis.Count < 1)
            {
                throw new Exception("No 'apis' specified");
            }

            var apiDefs = new List<dynamic>();

            foreach (var item in apis)
            {
                var path = (string) item.path;
                var defString = await apiSupplier(path);
                apiDefs.Add(JSON.DeserializeDynamic(defString));
            }

            var db = true;
        }

    }
}
