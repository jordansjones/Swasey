using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using Swasey.Model;
using Swasey.Tests.Generator;
using Swasey.Tests.ModelBuilder;

namespace Swasey.Tests.Helpers
{
    internal static class GenerationTestHelper
    {

        public const string DefaultBasePath = "/";

        public const string DefaultNamespace = "Swasey.Service.Client";

        public const string DefaultVersion = "1";

        public static IServiceDefinition DefaultServiceDefinition(this IGenerationTest This)
        {
            return This.ServiceBuilder().Build();
        }

        public static ServiceBuilder ServiceBuilder(this IGenerationTest This, string basePath = DefaultBasePath, string @namespace = DefaultNamespace, string version = DefaultVersion)
        {
            return new ServiceBuilder()
                .WithBasePath(DefaultBasePath)
                .WithNamespace(DefaultNamespace)
                .WithVersion(DefaultVersion);
        }

        public static IServiceDefinition CreateServiceClient(this IGenerationTest This, string name)
        {
            return This.ServiceBuilder()
                .WithName(name)
                .Build();
        }

        public static string Generate(this IServiceDefinition This)
        {
            using (var sw = new StringWriter())
            {
                This.WriteTo(sw);
                return sw.GetStringBuilder()
                    .ToString();
            }
        }

        public static SyntaxTree GenerateAndParse(this IServiceDefinition This)
        {
            return This.Generate()
                .AsSyntaxTree();
        }

        public static IEnumerable<T> GenerateAndGetParsedSyntaxNode<T>(this IServiceDefinition This)
            where T : SyntaxNode
        {
            return This.GenerateAndParse()
                .GetParsedSyntaxNode<T>();
        }

        private static readonly CSharpParseOptions ParseOptions = new CSharpParseOptions(LanguageVersion.CSharp5);

        public static SyntaxTree AsSyntaxTree(this string source)
        {
            return CSharpSyntaxTree.ParseText(source, ParseOptions);
        }

        public static IEnumerable<T> GetParsedSyntaxNode<T>(this SyntaxTree This)
            where T : SyntaxNode
        {
            return This
                .GetRoot()
                .DescendantNodes()
                .OfType<T>();
        }

    }
}
