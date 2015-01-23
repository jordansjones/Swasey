using System;
using System.Collections.Generic;
using System.Linq;

using Jil;

using Ploeh.AutoFixture;

using Swasey.Tests.Schema.Version12;

namespace Swasey.Tests.Helpers
{
    internal static class Fixtures
    {

        public static SingleFixtureBuilder Build
        {
            get { return new SingleFixtureBuilder(CreateAutoFixture()); }
        }

        public static T Create<T>()
        {
            return Build.One.Of<T>();
        }

        public static T Create<T>(T seed)
        {
            return Build.One.Of(seed);
        }

        public static Fixture CreateAutoFixture()
        {
            var fixture = new Fixture();
            fixture.Customize<string>(x => new StringGenerator(() => Guid.NewGuid().ToString("N")));
            return fixture;
        }

        public static string CreateResourceListingJson()
        {
            return JSON.SerializeDynamic(
                new
                {
                    apiVersion = ResourceListingVesion12.ApiVersion,
                    swaggerVersion = SwaggerVersion.Version12.Version(),
                    apis = new[]
                    {
                        new
                        {
                            path = ResourceListingVesion12.Apis_Pet_Path,
                            description = ResourceListingVesion12.Apis_Pet_Description
                        },
                        new
                        {
                            path = ResourceListingVesion12.Apis_User_Path,
                            description = ResourceListingVesion12.Apis_User_Description
                        },
                        new
                        {
                            path = ResourceListingVesion12.Apis_Store_Path,
                            description = ResourceListingVesion12.Apis_Store_Description
                        }
                    },
                    authorizations = new Dictionary<string, dynamic>
                    {
                        {
                            ResourceListingVesion12.Authorization_Type_OAuth2, new
                            {
                                type = ResourceListingVesion12.Authorization_Type_OAuth2,
                                scopes = new[]
                                {
                                    new
                                    {
                                        scope = ResourceListingVesion12.Scope_Email,
                                        description = ResourceListingVesion12.Scope_Email_Description
                                    },
                                    new
                                    {
                                        scope = ResourceListingVesion12.Scope_Pets,
                                        description = ResourceListingVesion12.Scope_Pets_Description
                                    }
                                },
                                grantTypes = new
                                {
                                    @implicit = new
                                    {
                                        loginEndpoint = new
                                        {
                                            url = ResourceListingVesion12.Url_LoginEndpoint
                                        }
                                    },
                                    authorization_code = new
                                    {
                                        tokenRequestEndpoint = new
                                        {
                                            url = ResourceListingVesion12.Url_TokenRequestEndpoint,
                                            clientIdName = ResourceListingVesion12.TokenRequestEndpoint_ClientIdName,
                                            clientSecretName = ResourceListingVesion12.TokenRequestEndpoint_ClientSecretName
                                        },
                                        tokenEndpoint = new
                                        {
                                            url = ResourceListingVesion12.Url_TokenEndpoint,
                                            tokenName = ResourceListingVesion12.TokenEndpoint_TokenName
                                        }
                                    }
                                }
                            }
                        }
                    },
                    info = new
                    {
                        title = ResourceListingVesion12.Info_Title,
                        description = ResourceListingVesion12.Info_Description,
                        termsOfServiceUrl = ResourceListingVesion12.Url_TermsOfServiceUrl,
                        contact = ResourceListingVesion12.Info_Contact,
                        license = ResourceListingVesion12.Info_License,
                        licenseUrl = ResourceListingVesion12.Url_LicenseUrl
                    }
                });
        }

    }
}
