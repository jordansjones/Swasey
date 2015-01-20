using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Swasey.Templates
{
    internal static class TemplateFactory
    {

        #region Boilerplate

        private static readonly string _resourceStreamFormat;

        private static readonly Assembly _thisAssembly;

        static TemplateFactory()
        {
            var thisType = typeof (TemplateFactory);
            _thisAssembly = thisType.Assembly;
            _resourceStreamFormat = new StringBuilder()
                .Append(thisType.Namespace)
                .Append('.').Append('{').Append(0).Append('}')
                .Append(".mustache")
                .ToString();
        }

        private static string ReadTemplate(string name)
        {
            var resourceName = string.Format(_resourceStreamFormat, name);
            using (var stream = _thisAssembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        #endregion


        private static string _fileHeader;

        public static string FileHeader
        {
            get { return _fileHeader ?? (_fileHeader = ReadTemplate("_FileHeader")); }
        }

        private static string _serviceClientClass;

        public static string ServiceClientClass
        {
            get
            {
                return _serviceClientClass ?? (_serviceClientClass = ReadTemplate("ServiceClientClass"));
            }
        }

    }
}
