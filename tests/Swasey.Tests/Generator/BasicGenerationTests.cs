using System;
using System.IO;
using System.Linq;
using System.Text;

using FluentAssertions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Swasey.Model;
using Swasey.Templates;
using Swasey.Tests.Helpers;

using Xunit;

namespace Swasey.Tests.Generator
{
    public class BasicGenerationTests
    {

        private const string DefaultNs = "Swasey.Service.Client";

        private static Services Model
        {
            get { return new Services("/", DefaultNs, "1"); }
        }

        [Fact]
        public void TestGeneratedFileIsValidCode()
        {
            var clientSource = ClientGenerator.GenerateClient(Model);

            var syntaxTree = CSharpSyntaxTree.ParseText(clientSource);

            syntaxTree.Should().NotBeNull("because the generated code should be parsable content");
            syntaxTree.HasCompilationUnitRoot.Should().BeTrue("because the generated code should be valid code content");
        }

        [Fact]
        public void TestGenerationProducesCorrectFileHeader()
        {
            var clientSource = ClientGenerator.GenerateClient(Model);
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
                        if (!isFirst) { sw.WriteLine(); }
                        x.WriteTo(sw);
                        isFirst = false;
                    }
                }
                    .Visit(codeRoot);
            }
            actual.ToString().Should().Be(TemplateFactory.FileHeader);
        }

        [Fact]
        public void TestGenerationProducesTheCorrectNamespace()
        {
            var clientSource = ClientGenerator.GenerateClient(Model);

            var nsSyntax = CSharpSyntaxTree.ParseText(clientSource)
                .GetRoot()
                .DescendantNodes()
                .OfType<NamespaceDeclarationSyntax>()
                .FirstOrDefault();

            nsSyntax.Should().NotBeNull("because a namespace was declared");


            nsSyntax.Name.ToString().Should().Be(DefaultNs);
        }

    }
}
