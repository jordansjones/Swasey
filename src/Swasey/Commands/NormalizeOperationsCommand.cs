using System;
using System.Linq;
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

            foreach (var normalOp in context.NormalizationContext.Operations.SelectMany(x => x))
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

                serviceDefinition.AddOperation(op);
            }
            

            var ctx = new LifecycleContext(context)
            {
                ServiceDefinition = serviceDefinition,
                State = LifecycleState.Continue
            };
            return Task.FromResult<ILifecycleContext>(ctx);
        }

        private QualifiedName ExtractName(INormalizationApiOperation op)
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

        private ParameterDefinition NormalizeParameterDefinition(INormalizationApiOperationParameter normalParam)
        {
            return new ParameterDefinition(normalParam.AsMetadata())
            {
                DataType = normalParam.AsDataType(),
                IsRequired = normalParam.IsRequired,
                IsVariadic = normalParam.AllowsMultiple,
                Name = normalParam.Name,
                Type = normalParam.ParameterType
            };
        }

        private ResponseDefinition NormalizeResponseDefinition(INormalizationApiOperation op)
        {
            var dt = op.Response.AsDataType();
            return new ResponseDefinition(op.Response.AsMetadata())
            {
                DataType = dt
            };
        }

        private OperationPath NormalizeOperationPath(INormalizationApiOperation op)
        {
            var path = op.Path;

            var versionParam = op.Parameters.FirstOrDefault(x => x.Name.IndexOf(Constants.ParameterName_Version, StringComparison.InvariantCultureIgnoreCase) >= 0);
            if (versionParam != null)
            {
                var template = new UriTemplate(path, true);
                template.SetParameter(versionParam.Name, op.ApiVersion);
                path = template.Resolve();
            }

            return new OperationPath(path);
        }

    }
}
