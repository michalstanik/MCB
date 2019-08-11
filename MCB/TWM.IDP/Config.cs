using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace TWM.IDP
{
    public static class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "fec0a4d6-5830-4eb8-8024-272bd5d6d2bb",
                    Username = "Michał",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Michał"),
                        new Claim("family_name", "Smith"),
                        new Claim("role", "Administrator")
                    }
                },
                new TestUser
                {
                    SubjectId = "c3b7f625-c07f-4d7d-9be1-ddff8ff93b4d",
                    Username = "Aga",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Aga"),
                        new Claim("family_name", "Smith"),
                        new Claim("role", "User")
                    }
                }
            };
        }

        public static List<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
               new IdentityResources.OpenId(),
               new IdentityResources.Profile(),
               new IdentityResource("roles", "Your role(s)", new []{"role"}),
            };
        }

        internal static IEnumerable<ApiResource> GetApiResources()
        {
            return new[] {
                new ApiResource("tripwithmeapi", "Trip With Me API", new[] { "role" })
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "mcb_api_swagger",
                    ClientName = "Swagger UI for MCB",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =
                    {
                        "http://localhost:9001/swagger-oauth2-redirect.html"
                    },
                    AllowedScopes = { "tripwithmeapi" }
                },
                new Client
                {
                    ClientName = "Trip With Me",
                    ClientId="tripwithmeclient",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 180,
                    RedirectUris =           { "https://localhost:9001/assets/oidc-login-redirect.html" },
                    PostLogoutRedirectUris = { "https://localhost:9001/?postLogout=true" },
                    AllowedCorsOrigins =     { "https://localhost:9001/" },
                    AllowedScopes = new []
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "tripwithmeapi"
                    }
                }
            };
        }
    }
}
