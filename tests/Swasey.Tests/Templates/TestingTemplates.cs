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
        
        private const string TemplateName_ServiceClientEnum = "ServiceClientEnum";
        private const string TemplateName_ServiceClientModel = "ServiceClientModel";
        private const string TemplateName_ServiceClientOperation = "ServiceClientOperation";

        internal const string TemplateName_ServiceClientInterface = "ServiceClientInterface";

        internal const string TemplateName_ServiceClientImplementation = "ServiceClientImplementation";

        public static string Template_ServiceClientEnum { get; private set; }

        public static string Template_ServiceClientModel { get; private set; }

        public static string Template_ServiceClientOperation { get; private set; }

        private static void Initialize()
        {
            // Templates
            SwaseyEngine.RegisterTemplate(TemplateName_ServiceClientInterface, ReadTemplate(TemplateName_ServiceClientInterface));
            SwaseyEngine.RegisterTemplate(TemplateName_ServiceClientImplementation, ReadTemplate(TemplateName_ServiceClientImplementation));

            // Enum Template
            Template_ServiceClientEnum = ReadTemplate(TemplateName_ServiceClientEnum);
            // Model Template
            Template_ServiceClientModel = ReadTemplate(TemplateName_ServiceClientModel);
            // Operation Template
            Template_ServiceClientOperation = ReadTemplate(TemplateName_ServiceClientOperation);
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
