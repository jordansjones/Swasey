using System;
using System.IO;
using System.Linq;

using Handlebars;

using Swasey.Helpers;

namespace Swasey
{
    public static class SwaseyEngine
    {

        static SwaseyEngine()
        {
            Engine = new Lazy<IHandlebars>(() => Handlebars.Handlebars.Create());

            // Initialize built-in handlers
            RegisterHelper(new FileHeader(HelperTemplates.HelperTemplate_FileHeader.Compile()));
            RegisterHelper(new UCFirst());
            RegisterHelper(new LCFirst());
        }

        internal static Lazy<IHandlebars> Engine { get; private set; }

        internal static Action<TextWriter, object> Compile(this Lazy<string> This)
        {
            return Compile(This.Value);
        }

        internal static Action<TextWriter, object> Compile(string template)
        {
            using (var sr = new StringReader(template))
            {
                return CompileTemplate(sr);
            }
        }

        public static Action<TextWriter, object> CompileTemplate(TextReader template)
        {
            return Engine.Value.Compile(template);
        }

        public static void RegisterHelper(IBlockHelper helper, string helperName = null)
        {
            helperName = helperName ?? helper.GetType().Name;
            Engine.Value.RegisterHelper(helperName, (tw, opts, ctx, args) => helper.Run(tw, opts, ctx, args));
        }

        public static void RegisterHelper(IInlineHelper helper, string helperName = null)
        {
            helperName = helperName ?? helper.GetType().Name;
            Engine.Value.RegisterHelper(helperName, (tw, ctx, args) => helper.Run(tw, ctx, args));
        }

        public static void RegisterTemplate(string name, string template)
        {
            using (var sr = new StringReader(template))
            {
                RegisterTemplate(name, sr);
            }
        }

        public static void RegisterTemplate(string name, TextReader template)
        {
            Engine.Value.RegisterTemplate(name, CompileTemplate(template));
        }

        public static string RenderRawTemplate(string template, object context)
        {
            return Engine.Value.Compile(template)(context);
        }

    }
}
