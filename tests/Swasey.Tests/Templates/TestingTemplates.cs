using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Swasey.Model;

namespace Swasey.Tests.Templates
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class TestingTemplates
    {

        internal const string TemplateName_ServiceClientMain = "ServiceClientMain";

        internal const string TemplateName_ServiceClientInterface = "ServiceClientInterface";

        internal const string TemplateName_ServiceClientImplementation = "ServiceClientImplementation";

        private static Lazy<Action<TextWriter, object>> Template_ServiceClientMain { get; set; }

        private static void Initialize()
        {
            // Templates
            SwaseyGenerator.RegisterTemplate(TemplateName_ServiceClientInterface, ReadTemplate(TemplateName_ServiceClientInterface));
            SwaseyGenerator.RegisterTemplate(TemplateName_ServiceClientImplementation, ReadTemplate(TemplateName_ServiceClientImplementation));

            // Main Template
            Template_ServiceClientMain = new Lazy<Action<TextWriter, object>>(
                () =>
                {
                    using (var sr = new StringReader(ReadTemplate(TemplateName_ServiceClientMain)))
                    {
                        return SwaseyGenerator.CompileTemplate(sr);
                    }
                });
        }

        public static IServiceDefinition WriteTo(this IServiceDefinition This, TextWriter output)
        {
            Template_ServiceClientMain.Value(output, This);
            return This;
        }

        #region Boilerplate

        private const string FileExtension = ".hbs";

        private static readonly string ResourceStreamFormat;

        private static readonly Assembly ThisAssembly;

        static TestingTemplates()
        {
            var thisType = typeof (TestingTemplates);
            ThisAssembly = thisType.Assembly;
            ResourceStreamFormat = new StringBuilder()
                .Append(thisType.Namespace)
                .Append('.').Append('{').Append(0).Append('}')
                .Append(FileExtension)
                .ToString();

            Initialize();
        }

        internal static string ReadTemplate(string name)
        {
            var resourceName = string.Format(ResourceStreamFormat, name);
            using (var stream = ThisAssembly.GetManifestResourceStream(resourceName))
            {
                Debug.Assert(stream != null, "stream != null");
                using (var sr = new StreamReader(stream))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        #endregion
    }
}
