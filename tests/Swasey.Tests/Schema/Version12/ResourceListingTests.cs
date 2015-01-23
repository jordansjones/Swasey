using System;

using FluentAssertions;

using System.Linq;

using Jil;

using Swasey.Schema.Version12.Metadata;
using Swasey.Tests.Helpers;

using Xunit;

namespace Swasey.Tests.Schema.Version12
{
    public class ResourceListingTests
    {


        [Fact(DisplayName = "Deserializer doesn't throw exceptions")]
        public void TestSchemaDeserializesWithoutException()
        {
            Fixtures.CreateResourceListingJson()
                .Invoking(x => JSON.Deserialize<ResourceListing>(x))
                .ShouldNotThrow<Exception>();
        }

        [Fact(DisplayName = "Schema deserialization works correctly")]
        public void TestSchemaDeserializationWorksCorrectly()
        {
            var schema = JSON.Deserialize<ResourceListing>(Fixtures.CreateResourceListingJson());

            schema.Should().NotBeNull("because the deserializer should throw exceptions instead of produce a null base object");

            schema.ApiVersion.Should().Be(ResourceListingVesion12.ApiVersion);
            schema.SwaggerVersion.Should().Be(SwaggerVersion.Version12);

            schema.Info.Should().NotBeNull();
            schema.Info.Title.Should().Be(ResourceListingVesion12.Info_Title);
            schema.Info.Description.Should().Be(ResourceListingVesion12.Info_Description);
            schema.Info.TermsOfServiceUrl.Should().Be(ResourceListingVesion12.Url_TermsOfServiceUrl);
            schema.Info.Contact.Should().Be(ResourceListingVesion12.Info_Contact);
            schema.Info.License.Should().Be(ResourceListingVesion12.Info_License);
            schema.Info.LicenseUrl.Should().Be(ResourceListingVesion12.Url_LicenseUrl);

            schema.Authorizations.Should().NotBeNull().And.HaveCount(1).And.ContainKey(ResourceListingVesion12.Authorization_Type_OAuth2);
            var oauth2 = schema.Authorizations[ResourceListingVesion12.Authorization_Type_OAuth2];
            oauth2.Should().NotBeNull();
            oauth2.AuthorizationType.Should().Be(ResourceListingVesion12.Authorization_Type_OAuth2);
            oauth2.KeyName.Should().BeNullOrEmpty();
            oauth2.PassAs.Should().BeNullOrEmpty();
            // Authorization Scopes
            oauth2.AuthorizationScopes.Should().HaveCount(2);
            oauth2.AuthorizationScopes[0].Name.Should().Be(ResourceListingVesion12.Scope_Email);
            oauth2.AuthorizationScopes[0].Description.Should().Be(ResourceListingVesion12.Scope_Email_Description);
            oauth2.AuthorizationScopes[1].Name.Should().Be(ResourceListingVesion12.Scope_Pets);
            oauth2.AuthorizationScopes[1].Description.Should().Be(ResourceListingVesion12.Scope_Pets_Description);
            // Grant Types
            oauth2.GrantTypes.Should().NotBeNull();
            oauth2.GrantTypes.Implicit.Should().NotBeNull();
            oauth2.GrantTypes.Implicit.LoginEndpoint.Should().NotBeNull();
            oauth2.GrantTypes.Implicit.LoginEndpoint.UrlAsUri().Should().NotBeNull().And.Be(new Uri(ResourceListingVesion12.Url_LoginEndpoint));
            oauth2.GrantTypes.AuthorizationCode.Should().NotBeNull();
            var tre = oauth2.GrantTypes.AuthorizationCode.TokenRequestEndpoint;
            var te = oauth2.GrantTypes.AuthorizationCode.TokenEndpoint;
            tre.Should().NotBeNull();
            tre.ClientIdName.Should().NotBeNullOrEmpty().And.Be(ResourceListingVesion12.TokenRequestEndpoint_ClientIdName);
            tre.ClientSecretName.Should().NotBeNullOrEmpty().And.Be(ResourceListingVesion12.TokenRequestEndpoint_ClientSecretName);
            tre.UrlAsUri().Should().NotBeNull().And.Be(new Uri(ResourceListingVesion12.Url_TokenRequestEndpoint));
            te.Should().NotBeNull();
            te.TokenName.Should().NotBeNullOrEmpty().And.Be(ResourceListingVesion12.TokenEndpoint_TokenName);
            te.UrlAsUri().Should().NotBeNull().And.Be(new Uri(ResourceListingVesion12.Url_TokenEndpoint));

            // Apis
            var expectedApis = new[]
            {
                KvPair.Of(ResourceListingVesion12.Apis_Pet_Path, ResourceListingVesion12.Apis_Pet_Description),
                KvPair.Of(ResourceListingVesion12.Apis_User_Path, ResourceListingVesion12.Apis_User_Description),
                KvPair.Of(ResourceListingVesion12.Apis_Store_Path, ResourceListingVesion12.Apis_Store_Description)
            };
            schema.Apis.Should().HaveCount(expectedApis.Length);
            for (var i = 0; i < expectedApis.Length; i++)
            {
                schema.Apis[i].Path.Should().NotBeNullOrWhiteSpace().And.Be(expectedApis[i].Key);
                schema.Apis[i].Description.Should().NotBeNullOrWhiteSpace().And.Be(expectedApis[i].Value);
            }
        }

    }
}