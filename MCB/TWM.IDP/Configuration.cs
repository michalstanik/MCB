﻿using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace TWM.IDP
{
    internal class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client> {
                new Client {
                    ClientId = "mcb_api_swagger",
                    ClientName = "Swagger UI for MCB",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 180,
                    RedirectUris =
                    {
                        "https://localhost:9001/api/oauth2-redirect.html"

                    },
                    AllowedScopes = new []
                    {
                        "tripwithmeapi"
                    }
                },
                new Client {
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

    internal class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource {
                    Name = "roles",
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> {
                new ApiResource("tripwithmeapi", "Trip With Me API", new[] { "roles" })
            };
        }
    }

    internal class Users
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser> {
                new TestUser {
                    SubjectId = "fec0a4d6-5830-4eb8-8024-272bd5d6d2bb",
                    Username = "Michal",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Michał"),
                        new Claim(JwtClaimTypes.Email, "scott@scottbrady91.com"),
                        new Claim("family_name", "Smith"),
                        new Claim("role", "Administrator")
                    }
                }
            };
        }
    }
}