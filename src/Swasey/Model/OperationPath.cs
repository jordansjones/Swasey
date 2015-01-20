using System;
using System.Collections.Generic;
using System.Linq;

namespace Swasey.Model
{
    public sealed class OperationPath : BasePath
    {

        private readonly Dictionary<ParameterName, IParameterDefinition> _pathParameters = new Dictionary<ParameterName, IParameterDefinition>();

        public OperationPath(string value)
            : base(value, IsValid) {}

        public OperationPath(string value, OperationPath toCopy)
            : base(value + toCopy, IsValid)
        {
            foreach (var kv in toCopy._pathParameters)
            {
                _pathParameters.Add(kv.Key, kv.Value);
            }
        }

        internal OperationPath AddPathParameter(IParameterDefinition paramDef)
        {
            _pathParameters[paramDef.Name] = paramDef;
            return this;
        }

        public static bool IsValid(string candidate)
        {
            return !string.IsNullOrWhiteSpace(candidate);
        }

        public static bool TryParse(string candidate, out OperationPath operationPath)
        {
            operationPath = null;

            if (!IsValid(candidate))
            {
                return false;
            }

            operationPath = new OperationPath(candidate);
            return true;
        }

        public static implicit operator string(OperationPath operationPath)
        {
            return operationPath.Value;
        }

    }
}
