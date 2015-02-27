using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Swasey.Lifecycle;
using Swasey.Model;
using Swasey.Normalization;

using Tavis.UriTemplates;

namespace Swasey.Commands
{
    internal class NormalizeOperationsCommand : ILifecycleCommand
    {

        public Task<ILifecycleContext> Execute(ILifecycleContext context)
        {
            var serviceDefinition = new ServiceDefinition(context.ServiceDefinition);

            foreach (var normalOp in context.NormalizationContext.Operations)
            {
                var op = new OperationDefinition(NormalizeOperationPath(normalOp), normalOp.AsMetadata())
                {
                    Description = normalOp.Description,
                    HttpMethod = normalOp.HttpMethod,
                    Name = ExtractName(normalOp),
                    ResourceName = normalOp.ResourcePath.ResourceNameFromPath(),
                    Response = NormalizeResponseDefinition(normalOp)
                };

                normalOp.Parameters
                    .Select(NormalizeParameterDefinition)
                    .ToList()
                    .ForEach(x => op.AddParameter(x));

                if (serviceDefinition.Operations.Any(x => x.ResourceName == op.ResourceName && x.Name == op.Name))
                {
                    var pathParams = op.Parameters.Where(x => x.Type == ParameterType.Path && x.IsRequired).OrderBy(x => x.Name);
                    op.Name += string.Join("And", pathParams.Select(x => "By" + x.Name.UCFirst()));
                }

                serviceDefinition.AddOperation(op);
            }


            var ctx = new LifecycleContext(context)
            {
                ServiceDefinition = serviceDefinition,
                State = LifecycleState.Continue
            };
            return Task.FromResult<ILifecycleContext>(ctx);
        }

        private QualifiedName ExtractName(NormalizationApiOperation op)
        {
            var name = op.Name;
            var resourceName = op.ResourcePath.ResourceNameFromPath();

            if (name.StartsWith(resourceName))
            {
                name = name.Substring(resourceName.Length);
            }

            if (name[0] == '_')
            {
                name = name.Substring(1);
            }

            if (char.IsLower(name[0]))
            {
                name = char.ToUpperInvariant(name[0]) + name.Substring(1);
            }

            return new QualifiedName(name);
        }

        private OperationPath NormalizeOperationPath(NormalizationApiOperation op)
        {
            var pathBuilder = new StringBuilder('/');

            var basePath = NormalizeBasePath(op.BasePath);
            if (!string.IsNullOrWhiteSpace(basePath))
            {
                pathBuilder.Append(basePath);
            }
            if (!string.IsNullOrWhiteSpace(op.Path))
            {
                var opPath = op.Path;
                if (opPath.StartsWith("/", StringComparison.InvariantCultureIgnoreCase))
                {
                    opPath = opPath.Length == 1 ? string.Empty : opPath.Substring(1);
                }
                pathBuilder.Append(opPath);
            }

            var path = pathBuilder.ToString();

            var versionParam = op.Parameters.FirstOrDefault(x => x.Name.IndexOf(Constants.ParameterName_Version, StringComparison.InvariantCultureIgnoreCase) >= 0);
            if (versionParam != null)
            {
                var template = new UriTemplate(path, true);
                template.SetParameter(versionParam.Name, op.ApiVersion);
                path = template.Resolve();
                op.Parameters.Remove(versionParam);
            }

            return new OperationPath(path);
        }

        private string NormalizeBasePath(string basePath)
        {
            if (string.IsNullOrWhiteSpace(basePath)) return null;

            try
            {
                var baseUri = new Uri(basePath, UriKind.RelativeOrAbsolute);
                basePath = baseUri.IsAbsoluteUri ? baseUri.AbsolutePath : basePath;

                var len = basePath.Length;
                if (len == 1 && basePath.Equals("/", StringComparison.InvariantCultureIgnoreCase)) return null;
                if (
                    len == 2
                    && basePath.StartsWith("/", StringComparison.InvariantCultureIgnoreCase)
                    && basePath.EndsWith("/", StringComparison.InvariantCultureIgnoreCase)
                    )
                {
                    return null;
                }

                basePath = basePath.StartsWith("/") ? basePath.Substring(1) : basePath;

                return basePath.EndsWith("/") ? basePath : basePath + "/";

            }
            catch (UriFormatException) { }
            return null;
        }

        private ParameterDefinition NormalizeParameterDefinition(NormalizationApiOperationParameter normalParam)
        {
            return new ParameterDefinition(normalParam.AsMetadata())
            {
                DataType = normalParam.AsDataType(),
                IsRequired = normalParam.IsRequired,
                IsVariadic = normalParam.AllowsMultiple,
                Name = normalParam.Name.MapDataTypeName(),
                Type = normalParam.ParameterType
            };
        }

        private ResponseDefinition NormalizeResponseDefinition(NormalizationApiOperation op)
        {
            var dt = op.Response.AsDataType();
            return new ResponseDefinition(op.Response.AsMetadata())
            {
                DataType = dt
            };
        }

    }
}
