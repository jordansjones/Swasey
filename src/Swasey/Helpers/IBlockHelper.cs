using System.IO;

using Handlebars;

namespace Swasey.Helpers
{
    public interface IBlockHelper
    {
        
        void Run(TextWriter output, HelperOptions options, dynamic context, object[] arguments); 

    }
}