using System;
using System.Linq;

using Handlebars;

namespace Swasey
{
    internal class GeneratorContext
    {
        #region Statics

        private static readonly Lazy<GeneratorContext> LazyInstance = new Lazy<GeneratorContext>(() => new GeneratorContext());

        public static GeneratorContext Instance
        {
            get { return LazyInstance.Value; }
        }

        public static IHandlebars Engine { get { return Instance._engine; } }

        #endregion


        private readonly IHandlebars _engine;

        private GeneratorContext()
        {
            _engine = Handlebars.Handlebars.Create();
        }


    }
}