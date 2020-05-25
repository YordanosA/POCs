// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("employee_info", new[] { "employee_id", "building_number", "office_number" })
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[] 
            {
                new ApiResource("Bandlay.API2", new[] { "employee_id" }),
                new ApiResource("DriveWithDan.API", new[] { "country" }),

                // expanded version if more control is needed
                new ApiResource
                {
                    /*http://docs.identityserver.io/en/3.1.0/reference/api_resource.html*/

                    Name = "Bandlay.API",

                    DisplayName = "Bandlay Core API",

                    Enabled = true,

                    // secret for using introspection endpoint
                    ApiSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // include the following using claims in access token (in addition to subject id)
                    UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.Email },

                    // this API defines two scopes
                    Scopes =
                    {
                        new Scope()
                        {
                            Name = "Bandlay.API.full_access",
                            DisplayName = "Full access to Bandlay API",
                            UserClaims = new[] { "full_access" }
                        },
                        new Scope
                        {
                            Name = "Bandlay.API.read_only",
                            DisplayName = "Read only access to Bandlay API",
                            UserClaims = new[] { "read_only" }
                        }
                    }
                    
                    //UserClaims = new[] { "my_api_claim" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "syncriver",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    //AllowedGrantTypes = GrantTypes.ClientCredentials,
                    RequireConsent = false,
                    RequirePkce = true,

                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "Bandlay.API.full_access",
                        "Bandlay.API.read_only",
                        "employee_info"
                    },

                    AllowOfflineAccess = true,

                    AlwaysIncludeUserClaimsInIdToken = true
                },
                new Client
                {
                    ClientId = "Bandlay.Web",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { { "Bandlay.API" }, { "fullAccess" }, { "readAccess" }, {"writeAccess" }, {"userAccess" } }

                }
            };
    }
}