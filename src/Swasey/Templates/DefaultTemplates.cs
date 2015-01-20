using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Handlebars;

namespace Swasey.Templates
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class DefaultTemplates
    {

        #region Boilerplate

        private const string FileExtension = ".hbs";

        internal static readonly Dictionary<string, Func<object, string>> TemplateCache = new Dictionary<string, Func<object, string>>();

        private static readonly string ResourceStreamFormat;

        private static readonly Assembly ThisAssembly;

        static DefaultTemplates()
        {
            var thisType = typeof (DefaultTemplates);
            ThisAssembly = thisType.Assembly;
            ResourceStreamFormat = new StringBuilder()
                .Append(thisType.Namespace)
                .Append('.').Append('{').Append(0).Append('}')
                .Append(FileExtension)
                .ToString();

            Initialize();
        }

        private static void CompileAndCacheTemplate(string name)
        {
            TemplateCache.Add(name, CompileTemplate(name));
        }

        private static Func<object, string> CompileTemplate(string name)
        {
            return GeneratorContext.Engine.Compile(ReadTemplate(name));
        }

        internal static TextReader ReadTemplateAsReader(string name)
        {
            return new StringReader(ReadTemplate(name));
        }

        internal static string ReadTemplate(string name)
        {
            var resourceName = string.Format(ResourceStreamFormat, name);
            using (var stream = ThisAssembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private static void RunHelper(Func<IHandlerContext, string> helper, TextWriter output, dynamic context, params object[] arguments)
        {
            output.WriteSafeString(helper(new DefaultTemplateContext(context ?? new ExpandoObject(), new List<object>(arguments))));
        }

        internal static Action<TextWriter, object> ServiceClientClassTemplate { get; private set; }

        #endregion

        #region Template Name Constants

        internal const string TemplateName_FileHeader = "FileHeader";

        internal const string TemplateName_ServiceClientMain = "ServiceClientMain";

        internal const string TemplateName_ServiceClientInterface = "ServiceClientInterface";

        internal const string TemplateName_ServiceClientImplementation = "ServiceClientImplementation";

        #endregion


        private static void Initialize()
        {
            // Templates
            CompileAndCacheTemplate(TemplateName_FileHeader);
            GeneratorContext.Engine.RegisterTemplate(TemplateName_FileHeader, Template_FileHeader);

            CompileAndCacheTemplate(TemplateName_ServiceClientInterface);
            GeneratorContext.Engine.RegisterTemplate(TemplateName_ServiceClientInterface, Template_ServiceClientInterface);

            CompileAndCacheTemplate(TemplateName_ServiceClientImplementation);
            GeneratorContext.Engine.RegisterTemplate(TemplateName_ServiceClientImplementation, Template_ServiceClientImplementation);


            // Main Template
            ServiceClientClassTemplate = GeneratorContext.Engine.Compile(ReadTemplateAsReader(TemplateName_ServiceClientMain));
        }


        #region FileHeader

        public static Func<IHandlerContext, string> FileHeader { get; set; }

        private static void Template_FileHeader(TextWriter output, dynamic context)
        {
            var helper = FileHeader ?? (x => TemplateCache[TemplateName_FileHeader](x));
            RunHelper(helper, output, context);
        }

        #endregion

        #region Service Client Interface

        private static void Template_ServiceClientInterface(TextWriter output, object context)
        {
            var content = TemplateCache[TemplateName_ServiceClientInterface](context);
            output.WriteSafeString(content);
        }

        #endregion

        #region Service Client Implementation

        private static void Template_ServiceClientImplementation(TextWriter output, object context)
        {
            var content = TemplateCache[TemplateName_ServiceClientImplementation](context);
            output.WriteSafeString(content);
        }

        #endregion


    }
}
