using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Constants;

namespace ServerBE.IdentityServer4
{
    public static class IS4Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        //public static IEnumerable<ApiResource> ApiResources =>
        //    new List<ApiResource>
        //    {
        //    new ApiResource("ShoppingAPI","Test API Scope")
        //    };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("shoppingAPI","Shopping API Scope")
            };

        public static IEnumerable<Client> Clients => 
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { ConstantUri.CUSTOMER_SITE_URL + "signin-oidc" },

                    PostLogoutRedirectUris = { ConstantUri.CUSTOMER_SITE_URL + "signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "shoppingAPI"
                    }
                },

                new Client
                    {
                        ClientId = "swagger",
                        ClientSecrets = { new Secret("secret".Sha256()) },
                        AllowedGrantTypes = GrantTypes.Code,

                        RequireConsent = false,
                        RequirePkce = true,

                        RedirectUris =           { $"{ConstantUri.CUSTOMER_SITE_URL}swagger/oauth2-redirect.html" },
                        PostLogoutRedirectUris = { $"{ConstantUri.CUSTOMER_SITE_URL}swagger/oauth2-redirect.html" },
                        AllowedCorsOrigins =     { ConstantUri.CUSTOMER_SITE_URL },

                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "shoppingAPI"
                        }
                    },

                new Client {
                    ClientName = "admin",
                    ClientId = "admin",
                    // AccessTokenType = AccessTokenType.Reference,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    // RequirePkce = true,
                    AlwaysSendClientClaims = true,
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken = true,

                    RedirectUris = new List<string>
                    {
                        $"{ ConstantUri.ADMIN_PAGE_URL }/authentication/login-callback",
                        $"{ ConstantUri.ADMIN_PAGE_URL }/silent-renew.html",
                        ConstantUri.ADMIN_PAGE_URL
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        $"{ ConstantUri.ADMIN_PAGE_URL }/authentication/logout-callback",
                        $"{ ConstantUri.ADMIN_PAGE_URL }/unauthorized",
                        ConstantUri.ADMIN_PAGE_URL
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        ConstantUri.ADMIN_PAGE_URL
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "shoppingAPI",
                    }
                }
            };
    }
}
