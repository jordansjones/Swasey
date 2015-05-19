using System;
using System.IO;
using System.Linq;

using HandlebarsDotNet;

namespace Swasey.Helpers
{
    public interface IBlockHelper
    {

        void Run(TextWriter output, HelperOptions options, dynamic context, object[] arguments);

    }
}
