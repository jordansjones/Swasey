using System;
using System.IO;
using System.Linq;

using Swasey.Model;

namespace Swasey.Helpers
{
    internal class OperationParameters : IInlineHelper
    {

        private readonly Action<TextWriter, object> _template;

        public OperationParameters(Action<TextWriter, object> template)
        {
            _template = template;
        }

        public void Run(TextWriter output, dynamic context, object[] arguments)
        {
            var ctx = context as IOperationDefinition;
            if (ctx == null) return;

            var parameters = ctx.Parameters.Where(x => !Constants.ParameterName_Version.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))
                .Where(x => x.Type != ParameterType.Header)
                .ToList();
            _template(output, parameters);
        }

    }
}
