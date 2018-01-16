using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    public sealed class OperationPath20 : BasePath
    {

        private readonly Dictionary<ParameterName, IParameterDefinition> _pathParameters = new Dictionary<ParameterName, IParameterDefinition>();

        public OperationPath20(string value)
            : base(value, IsValid) {}

        public OperationPath20(string value, OperationPath20 toCopy)
            : base(NormalizePaths(value, toCopy), IsValid)
        {
            foreach (var kv in toCopy._pathParameters)
            {
                _pathParameters.Add(kv.Key, kv.Value);
            }
        }

        internal OperationPath20 AddPathParameter(IParameterDefinition paramDef)
        {
            _pathParameters[paramDef.Name] = paramDef;
            return this;
        }

        public static bool IsValid(string candidate)
        {
            return !string.IsNullOrWhiteSpace(candidate);
        }

        public static bool TryParse(string candidate, out OperationPath20 operationPath)
        {
            operationPath = null;

            if (!IsValid(candidate))
            {
                return false;
            }

            operationPath = new OperationPath20(candidate);
            return true;
        }

        public static implicit operator string(OperationPath20 operationPath)
        {
            return operationPath.Value;
        }

        private static string NormalizePaths(string basePath, OperationPath20 toCopy)
        {
            var operationPath = toCopy.ToString();
            if (basePath.EndsWith("/") && operationPath.StartsWith("/"))
            {
                operationPath = operationPath.Substring(1);
            }
            return basePath + operationPath;
        }

    }
}
