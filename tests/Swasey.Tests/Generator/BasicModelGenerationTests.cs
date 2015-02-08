using System;
using System.IO;

using FluentAssertions;

using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Swasey.Helpers;
using Swasey.Model;
using Swasey.Tests.Helpers;

using Xunit;

namespace Swasey.Tests.Generator
{
//    public class BasicModelGenerationTests
//    {
//
//        [Fact(DisplayName = "Generated file is valid code")]
//        public void TestGeneratedFileIsValidCode()
//        {
//            var clientSource = this.DefaultServiceDefinition().Generate();
//            clientSource.Should().NotBeNullOrWhiteSpace("because there should be some generated content");
//
//            var syntaxTree = clientSource.AsSyntaxTree();
//
//            syntaxTree.Should().NotBeNull("because the generated code should be parsable content");
//            syntaxTree.HasCompilationUnitRoot.Should().BeTrue("because the generated code should be valid code content");
//        }
//
//        [Fact(DisplayName = "Generation produces correct file header")]
//        public void TestGenerationProducesCorrectFileHeader()
//        {
//            var clientSource = this.DefaultServiceDefinition().Generate();
//            var actual = new StringBuilder();
//
//            using (var sw = new StringWriter(actual))
//            {
//                var isFirst = true;
//                var codeRoot = CSharpSyntaxTree.ParseText(clientSource).GetRoot();
//                new SyntaxTriviaWalker
//                {
//                    TriviaPredicate = x => x.CSharpKind() == SyntaxKind.SingleLineCommentTrivia,
//                    TriviaAction = x =>
//                    {
//                        // ReSharper disable AccessToDisposedClosure
//                        if (!isFirst) { sw.WriteLine(); }
//                        x.WriteTo(sw);
//                        isFirst = false;
//                        // ReSharper restore AccessToDisposedClosure
//                    }
//                }
//                    .Visit(codeRoot);
//            }
//            actual.ToString().Should().Be(HelperTemplates.HelperTemplate_FileHeader.Value);
//        }
//
//        [Fact(DisplayName = "Generation produces the correct namespace")]
//        public void TestGenerationProducesTheCorrectNamespace()
//        {
//            NamespaceDeclarationSyntax node = null;
//            this.DefaultServiceDefinition().GenerateAndGetParsedSyntaxNode<NamespaceDeclarationSyntax>()
//                .Invoking(x => node = x.First())
//                .ShouldNotThrow<InvalidOperationException>("because a Namespace was declared");
//
//            node.Name.ToString().Should().Be(GenerationTestHelper.DefaultNamespace, "because that is the namespace we defined");
//        }
//
//        [Fact(DisplayName = "Generation produces a service interface")]
//        public void TestGenerationProducesServiceInterface()
//        {
//            var serviceName = Fixtures.Create("service");
//            InterfaceDeclarationSyntax node = null;
//            this.CreateServiceClient(serviceName).GenerateAndGetParsedSyntaxNode<InterfaceDeclarationSyntax>()
//                .Invoking(x => node = x.First())
//                .ShouldNotThrow<InvalidOperationException>("because an Interface was declared");
//
//            var expected = Char.ToUpper(serviceName[0]) + serviceName.Substring(1);
//
//            node.Identifier.ValueText.Should().Be("I" + expected, "because that is the name we defined for the service client");
//        }
//
//        [Fact(DisplayName = "Generation produces a service implementation")]
//        public void TestGenerationProducesServiceImplementation()
//        {
//            var serviceName = Fixtures.Create("Service");
//            ClassDeclarationSyntax node = null;
//            this.CreateServiceClient(serviceName).GenerateAndGetParsedSyntaxNode<ClassDeclarationSyntax>()
//                .Invoking(x => node = x.First())
//                .ShouldNotThrow<InvalidOperationException>("because a Class was declared");
//
//            var expected = new QualifiedName(serviceName);
//
//            node.Identifier.ValueText.Should().Be(expected.ToString(), "because that is the name we defined for the service client");
//            node.Modifiers.Should().HaveCount(1, "because the service client implementation is only internal");
//            node.Modifiers.First().CSharpKind().Should().Be(SyntaxKind.InternalKeyword);
//
//            node.BaseList.Types.Should().HaveCount(1, "because the service client only implements the service client interface");
//            node.BaseList.Types.First().Type.As<IdentifierNameSyntax>()
//                .Identifier.ValueText.Should().Be("I" + expected, "because that is the interface name for the service client");
//        }
//
//        [Fact(DisplayName = "Generation with operation definitions produces a service interface methods")]
//        public void TestGenerationProducesServiceInterfaceMethods()
//        {
//            var serviceName = Fixtures.Create("Service");
//            var opNames = Fixtures.Build.Three.Of("Operation").ToArray();
//
//            var generatedContent = this.ServiceBuilder()
//                .WithName(serviceName)
//                .WithOperation(
//                    ob => ob.WithHttpMethod(HttpMethodType.GET)
//                        .WithName(opNames[0])
//                        .WithPath("/")
//                        .WithResponse(r => r.WithVoidDataType())
//                )
//                .WithOperation(
//                    ob => ob.WithHttpMethod(HttpMethodType.DELETE)
//                        .WithName(opNames[1])
//                        .WithDescription(string.Format("This is the summary for Operation '{0}'", opNames[1]))
//                        .WithPath("/")
//                        .WithResponse(r => r.WithVoidDataType())
//                )
//                .WithOperation(
//                    ob => ob.WithHttpMethod(HttpMethodType.DELETE)
//                        .WithName(opNames[2])
//                        .WithPath("/")
//                        .WithResponse(r => r.WithVoidDataType())
//                )
//                .Build()
//                .Generate();
//
//            generatedContent.Should().NotBeNullOrWhiteSpace("because we defined things to generate");
//
//            var interfaceNode = generatedContent
//                .AsSyntaxTree()
//                .GetParsedSyntaxNode<InterfaceDeclarationSyntax>()
//                .First();
//
//            interfaceNode.Members.Should().HaveCount(3, "because that is how many operations were defined");
//
//            interfaceNode.Members[0].Should().BeOfType<MethodDeclarationSyntax>()
//                .Which.Identifier.ValueText.Should().Be(opNames[0]);
//
//            interfaceNode.Members[1].Should().BeOfType<MethodDeclarationSyntax>()
//                .Which.Identifier.ValueText.Should().Be(opNames[1]);
//
//            interfaceNode.Members[2].Should().BeOfType<MethodDeclarationSyntax>()
//                .Which.Identifier.ValueText.Should().Be(opNames[2]);
//        }
//
//    }
}
