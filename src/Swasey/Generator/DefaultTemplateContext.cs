using System.Collections.Generic;

namespace Swasey
{
    internal class DefaultTemplateContext : IHandlerContext
    {

        private readonly List<object> _arguments;

        public DefaultTemplateContext(dynamic model, List<object> arguments)
        {
            Model = model;
            _arguments = arguments;
        }

        public dynamic Model { get; private set; }

        public IReadOnlyList<object> Arguments { get { return _arguments; } }

    }
}