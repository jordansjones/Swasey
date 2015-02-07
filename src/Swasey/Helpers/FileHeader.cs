using System;
using System.IO;
using System.Linq;

namespace Swasey.Helpers
{
    internal class FileHeader : IInlineHelper
    {

        private readonly Action<TextWriter, object> _template;

        public FileHeader(Action<TextWriter, object> template)
        {
            _template = template;
        }

        public void Run(TextWriter output, dynamic context, object[] arguments)
        {
            _template(output, context);
        }

    }
}
