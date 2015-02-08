using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using Swasey.Model;
using Swasey.Tests.ModelBuilder;
using Swasey.Tests.Templates;

namespace Swasey.Tests.Helpers
{
    internal static class GenerationTestHelper
    {

        private static readonly CSharpParseOptions ParseOptions = new CSharpParseOptions(LanguageVersion.CSharp5);

//        public static string Generate(this IServiceDefinition This)
//        {
//            using (var sw = new StringWriter())
//            {
//                This.WriteTo(sw);
//                return sw.GetStringBuilder()
//                    .ToString();
//            }
//        }
//
//        public static SyntaxTree GenerateAndParse(this IServiceDefinition This)
//        {
//            return This.Generate()
//                .AsSyntaxTree();
//        }
//
//        public static IEnumerable<T> GenerateAndGetParsedSyntaxNode<T>(this IServiceDefinition This)
//            where T : SyntaxNode
//        {
//            return This.GenerateAndParse()
//                .GetParsedSyntaxNode<T>();
//        }

        public static SyntaxTree AsSyntaxTree(this string source, SourceCodeKind sourceKind = SourceCodeKind.Regular)
        {
            return CSharpSyntaxTree.ParseText(source, ParseOptions.WithKind(sourceKind));
        }

        public static IServiceDefinition CreateServiceClient(this object This, string name)
        {
            return This.ServiceBuilder()
                .WithName(name)
                .Build();
        }

        public static GeneratorOptions DefaultGeneratorOptions(Func<Uri, Task<string>> jsonLoader, ITestSwaseyWriter writer)
        {
            return new TestGeneratorOptions(new TestSwaggerJsonLoader(jsonLoader), writer ?? new StringBuilderSwaseyWriter())
            {
                ApiNamespace = Fixtures.DefaultApiNamespace,
                ModelNamespace = Fixtures.DefaultModelNamespace,
                ApiEnumTemplate = TestingTemplates.Template_ServiceClientEnum,
                ApiModelTemplate = TestingTemplates.Template_ServiceClientModel,
                ApiOperationTemplate = TestingTemplates.Template_ServiceClientOperation
            };
        }

        public static IServiceDefinition DefaultServiceDefinition(this object This)
        {
            return This.ServiceBuilder().Build();
        }

        public static IEnumerable<T> GetParsedSyntaxNode<T>(this SyntaxTree This)
            where T : SyntaxNode
        {
            return This
                .GetRoot()
                .DescendantNodes()
                .OfType<T>();
        }

        public static ServiceBuilder ServiceBuilder(
            this object This,
            string basePath = Fixtures.DefaultBasePath,
            string apiNamespace = Fixtures.DefaultApiNamespace,
            string modelNamespace = Fixtures.DefaultModelNamespace,
            string version = Fixtures.DefaultVersion)
        {
            return new ServiceBuilder()
                .WithApiNamespace(apiNamespace)
                .WithModelNamespace(modelNamespace)
                .WithVersion(version);
        }

    }
}
