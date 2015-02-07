using System;
using System.IO;
using System.Linq;

namespace Swasey.Helpers
{
    public interface IInlineHelper
    {

        void Run(TextWriter output, dynamic context, object[] arguments);

    }
}
