using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using FluentAssertions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Swasey.Model;
using Swasey.Tests.Helpers;
using Swasey.Tests.Templates;

using Xunit;
using Xunit.Extensions;

namespace Swasey.Tests.Generator
{
    public class BasicGenerationTests : IGenerationTest
    {

        [Fact(DisplayName = "Generated file is valid code")]
        public void TestGeneratedFileIsValidCode()
        {
            var clientSource = this.DefaultServiceDefinition().Generate();
            clientSource.Should().NotBeNullOrWhiteSpace("because there should be some generated content");

            var syntaxTree = clientSource.AsSyntaxTree();

            syntaxTree.Should().NotBeNull("because the generated code should be parsable content");
            syntaxTree.HasCompilationUnitRoot.Should().BeTrue("because the generated code should be valid code content");
        }

        [Fact(DisplayName = "Generation produces correct file header")]
        public void TestGenerationProducesCorrectFileHeader()
        {
            var clientSource = this.DefaultServiceDefinition().Generate();
            var actual = new StringBuilder();

            using (var sw = new StringWriter(actual))
            {
                var isFirst = true;
                var codeRoot = CSharpSyntaxTree.ParseText(clientSource).GetRoot();
                new SyntaxTriviaWalker
                {
                    TriviaPredicate = x => x.CSharpKind() == SyntaxKind.SingleLineCommentTrivia,
                    TriviaAction = x =>
                    {
                        // ReSharper disable AccessToDisposedClosure
                        if (!isFirst) { sw.WriteLine(); }
                        x.WriteTo(sw);
                        isFirst = false;
                        // ReSharper restore AccessToDisposedClosure
                    }
                }
                    .Visit(codeRoot);
            }
            actual.ToString().Should().Be(DefaultTemplates.ReadTemplate(DefaultTemplates.TemplateName_FileHeader));
        }

        [Fact(DisplayName = "Generation produces the correct namespace")]
        public void TestGenerationProducesTheCorrectNamespace()
        {
            NamespaceDeclarationSyntax node = null;
            this.DefaultServiceDefinition().GenerateAndGetParsedSyntaxNode<NamespaceDeclarationSyntax>()
                .Invoking(x => node = x.First())
                .ShouldNotThrow<InvalidOperationException>("because a Namespace was declared");

            node.Name.ToString().Should().Be(GenerationTestHelper.DefaultNamespace, "because that is the namespace we defined");
        }

        [Theory(DisplayName = "Generation produces a service interface")]
        [AutoMoq]
        public void TestGenerationProducesServiceInterface(string serviceName)
        {
            InterfaceDeclarationSyntax node = null;
            this.CreateServiceClient(serviceName).GenerateAndGetParsedSyntaxNode<InterfaceDeclarationSyntax>()
                .Invoking(x => node = x.First())
                .ShouldNotThrow<InvalidOperationException>("because an Interface was declared");
            
            var expected = new QualifiedName(serviceName);

            node.Identifier.ValueText.Should().Be("I" + expected, "because that is the name we defined for the service client");
        }

        [Theory(DisplayName = "Generation produces a service implementation")]
        [AutoMoq]
        public void TestGenerationProducesServiceImplementation(string serviceName)
        {
            ClassDeclarationSyntax node = null;
            this.CreateServiceClient(serviceName).GenerateAndGetParsedSyntaxNode<ClassDeclarationSyntax>()
                .Invoking(x => node = x.First())
                .ShouldNotThrow<InvalidOperationException>("because a Class was declared");
            
            var expected = new QualifiedName(serviceName);

            node.Identifier.ValueText.Should().Be(expected.ToString(), "because that is the name we defined for the service client");
            node.Modifiers.Should().HaveCount(1, "because the service client implementation is only internal");
            node.Modifiers.First().CSharpKind().Should().Be(SyntaxKind.InternalKeyword);

            node.BaseList.Types.Should().HaveCount(1, "because the service client only implements the service client interface");
            node.BaseList.Types.First().Type.As<IdentifierNameSyntax>()
                .Identifier.ValueText.Should().Be("I" + expected, "because that is the interface name for the service client");
        }

        [Theory(DisplayName = "Generation with operation definitions produces a service interface methods")]
        [AutoMoq]
        public void TestGenerationProducesServiceInterfaceMethods(string serviceName, string opName1, string opName2, string opName3)
        {
            var expectedOpName1 = new QualifiedName(opName1);
            var expectedOpName2 = new QualifiedName(opName2);
            var expectedOpName3 = new QualifiedName(opName3);

            var interfaceNode = this.ServiceBuilder()
                .WithName(serviceName)
                .WithOperation(
                    ob => ob.WithHttpMethod(HttpMethodType.GET)
                        .WithName(opName1)
                        .WithPath("/")
                        .WithResponse(r => r.WithDataType("void"))
                )
                .WithOperation(
                    ob => ob.WithHttpMethod(HttpMethodType.DELETE)
                        .WithName(opName2)
                        .WithDescription(string.Format("This is the summary for Operation '{0}'", opName2))
                        .WithPath("/")
                        .WithResponse(r => r.WithDataType("void"))
                )
                .WithOperation(
                    ob => ob.WithHttpMethod(HttpMethodType.DELETE)
                        .WithName(opName3)
                        .WithPath("/")
                        .WithResponse(r => r.WithDataType("void"))
                )
                .Build()
                .Generate()
                .AsSyntaxTree()
                .GetParsedSyntaxNode<InterfaceDeclarationSyntax>()
                .First();

            interfaceNode.Members.Should().HaveCount(3, "because that is how many operations were defined");

            (interfaceNode.Members[0] as MethodDeclarationSyntax).Identifier.ValueText.Should().Be(expectedOpName1.ToString());
            (interfaceNode.Members[1] as MethodDeclarationSyntax).Identifier.ValueText.Should().Be(expectedOpName2.ToString());
            (interfaceNode.Members[2] as MethodDeclarationSyntax).Identifier.ValueText.Should().Be(expectedOpName3.ToString());
        }

    }
}
