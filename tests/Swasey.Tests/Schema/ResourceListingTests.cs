using System;
using System.Linq;

using FluentAssertions;

using Jil;

using Swasey.Schema;
using Swasey.Tests.Helpers;

using Xunit;

namespace Swasey.Tests.Schema
{
    public class ResourceListingTests
    {


        [Fact]
        public void TestSchemaDeserializesWithoutException()
        {
            CreateFixture()
                .Invoking(x => JSON.Deserialize<ResourceListing>(x))
                .ShouldNotThrow<Exception>();
        }

        [Fact]
        public void TestSchemaDeserializationWorksCorrectly()
        {
            var schema = JSON.Deserialize<ResourceListing>(CreateFixture());

            schema.Should().NotBeNull("because the deserializer should throw exceptions");

            schema.ApiVersion.Should().Be("1.0.0");
            schema.SwaggerVersion.Should().Be("1.2");

            schema.ApiInfo.Should().NotBeNull();
            schema.ApiInfo.Title.Should().Be("Swagger Sample App");
            schema.ApiInfo.Description.Should().Be("This is a sample server Petstore server.  You can find out more about Swagger \n    at <a href=\"http://swagger.wordnik.com\">http://swagger.wordnik.com</a> or on irc.freenode.net, #swagger.  For this sample,\n    you can use the api key \"special-key\" to test the authorization filters");
            schema.ApiInfo.TermsOfServiceUrl.Should().Be("http://helloreverb.com/terms/");
            schema.ApiInfo.Contact.Should().Be("apiteam@wordnik.com");
            schema.ApiInfo.License.Should().Be("Apache 2.0");
            schema.ApiInfo.LicenseUrl.Should().Be("http://www.apache.org/licenses/LICENSE-2.0.html");

            schema.Authorizations.Should().NotBeNull().And.HaveCount(1).And.ContainKey("oauth2");
            var oauth2 = schema.Authorizations["oauth2"];
            oauth2.Should().NotBeNull();
            oauth2.AuthorizationType.Should().Be("oauth2");
            oauth2.KeyName.Should().BeNullOrEmpty();
            oauth2.PassAs.Should().BeNullOrEmpty();
            oauth2.AuthorizationScopes.Should().HaveCount(0);
        }


        private static string CreateFixture()
        {
            return GhettoJsonCreator.Object(x => x
                .Value("apiVersion", "1.0.0")
                .Value("swaggerVersion", "1.2")
                .Array("apis", apis => apis
                    .Object(o => o.Value("path", "/pet").Value("description", "Operations about pets"))
                    .Object(o => o.Value("path", "/user").Value("description", "Operations about user"))
                    .Object(o => o.Value("path", "/store").Value("description", "Operations about store"))
                )
                .Object("authorizations", auths => auths
                    .Object("oauth2", o => o
                        .Value("type", "oauth2")
                        .Array("scopes", scopes => scopes
                            .Object(scope => scope.Value("scope", "email").Value("description", "Access to your email address"))
                            .Object(scope => scope.Value("scope", "pets").Value("description", "Access to your pets"))
                        )
                        .Object("grantTypes", gt => gt
                            .Object("implicit", grant => grant
                                .Object("loginEndpoint", le => le.Value("url", "http://petstore.swagger.wordnik.com/oauth/dialog"))
                            )
                            .Object("authorization_code", grant => grant
                                .Object("tokenRequestEndpoint", tre => tre
                                    .Value("url", "http://petstore.swagger.wordnik.com/oauth/requestToken")
                                    .Value("clientIdName", "client_id")
                                    .Value("clientSecretName", "client_secret")
                                )
                                .Object("tokenEndpoint", te => te
                                    .Value("url", "http://petstore.swagger.wordnik.com/oauth/token")
                                    .Value("tokenName", "access_code")
                                )
                            )
                        )
                    )
                )
                .Object("info", info => info
                    .Value("title", "Swagger Sample App")
                    .Value("description", "This is a sample server Petstore server.  You can find out more about Swagger \n    at <a href=\"http://swagger.wordnik.com\">http://swagger.wordnik.com</a> or on irc.freenode.net, #swagger.  For this sample,\n    you can use the api key \"special-key\" to test the authorization filters")
                    .Value("termsOfServiceUrl", "http://helloreverb.com/terms/")
                    .Value("contact", "apiteam@wordnik.com")
                    .Value("license", "Apache 2.0")
                    .Value("licenseUrl", "http://www.apache.org/licenses/LICENSE-2.0.html")
                )
            );
        }

    }
}