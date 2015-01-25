using System;

using FluentAssertions;

using System.Linq;

using Microsoft.CodeAnalysis;

using Ploeh.AutoFixture;

using Swasey.Model;
using Swasey.Tests.Helpers;
using Swasey.Tests.ModelBuilder;

using Tavis.UriTemplates;

using Xunit;

namespace Swasey.Tests.Generator
{
    public class OperationParametersHelperTests
    {

        private const string TestTemplate = @"{{OperationParameters}}";

        private const string CancellationTokenParameter = @"CancellationToken? cancelToken = null";

        [Fact(DisplayName = "Operation with only version path parameter only generates CancellationToken method parameters")]
        public void OperationWithSinglePathParameterReturnsOnlyCancelationTokenMethodParameter()
        {
            var model = new OperationBuilder()
                .WithVersion(string.Format("v{0}", Fixtures.Create<int>()))
                .WithParameter(p => p.WithType(ParameterType.Path).WithDataType("string").WithName("version"))
                .WithName("Operation")
                .WithPath("/{version}/operation/{id}")
                .WithVoidResponse()
                .Build();

            var actual = SwaseyEngine.RenderRawTemplate(TestTemplate, model);

            var root = actual.AsSyntaxTree(SourceCodeKind.Script)
                .GetRoot();

            var children = root.ChildNodes().ToList();
            children.Should().HaveCount(1, "because there should only be the CancelToken parameter");

            children[0].ToString().Should().Be(CancellationTokenParameter);
        }

        [Fact(DisplayName = "Header Parameter types should not be generated as method input parameters")]
        public void OperationHeaderParmetersShouldNotBeMethodParameters()
        {
            var model = new OperationBuilder()
                .WithName("Operation")
                .WithPath("/Operation")
                .WithVoidResponse()
                .WithParameter(p => p.WithType(ParameterType.Header).WithDataType("string").WithName("x-force-delete"))
                .WithParameter(p => p.WithType(ParameterType.Header).WithDataType("string").WithName("x-signalr-token"))
                .Build();

            var actual = SwaseyEngine.RenderRawTemplate(TestTemplate, model);

            actual.Should().Be(CancellationTokenParameter, "because we only specified header parameters");
        }

    }
}
