using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Swasey.Tests.Templates
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class DefaultTemplates
    {

        #region Boilerplate

        private const string FileExtension = ".template";

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

        private static void ReadAndCachePartialTemplate(string name)
        {
            Handlebars.Handlebars.RegisterTemplate(name, CompileTemplate(name));
        }

        private static Action<TextWriter, object> CompileTemplate(string name)
        {
            return Handlebars.Handlebars.Compile(ReadTemplateAsReader(name));
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
            ReadAndCachePartialTemplate(TemplateName_FileHeader);
            ReadAndCachePartialTemplate(TemplateName_ServiceClientInterface);
            ReadAndCachePartialTemplate(TemplateName_ServiceClientImplementation);


            // Main Template
            Template_ServiceClientMain = CompileTemplate(TemplateName_ServiceClientMain);
        }

        internal static Action<TextWriter, Object> Template_ServiceClientMain { get; private set; }


    }
}
