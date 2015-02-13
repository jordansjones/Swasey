using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Swasey.Helpers
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class HelperTemplates
    {

        internal const string TemplateName_FileHeader = "FileHeader";

        internal static Lazy<string> HelperTemplate_FileHeader { get; private set; }

        private static void Initialize()
        {
            // Helper Templates
            HelperTemplate_FileHeader = new Lazy<string>(() => ReadTemplate(TemplateName_FileHeader));
        }

        #region Boilerplate

        private const string FileExtension = ".hbs";

        private static readonly string ResourceStreamFormat;

        private static readonly Assembly ThisAssembly;

        static HelperTemplates()
        {
            var thisType = typeof (HelperTemplates);
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
