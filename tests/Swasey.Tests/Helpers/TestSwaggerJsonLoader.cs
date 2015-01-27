using System;
using System.Linq;
using System.Threading.Tasks;

namespace Swasey.Tests.Helpers
{
    public class TestSwaggerJsonLoader
    {

        public TestSwaggerJsonLoader(Func<Uri, Task<string>> jsonLoader)
        {
            JsonLoader = jsonLoader;
        }

        public Func<Uri, Task<String>> JsonLoader { get; private set; }

        public Task<string> Load(Uri uri)
        {
            return JsonLoader(uri);
        }

    }
}
