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

                    RedirectUris = { "https://localhost:4001/" + "signin-oidc" },

                    PostLogoutRedirectUris = { "https://localhost:4001/" + "signout-callback-oidc" },

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

                        RedirectUris =           { $"https://localhost:4001/swagger/oauth2-redirect.html" },
                        PostLogoutRedirectUris = { $"https://localhost:4001/swagger/oauth2-redirect.html" },
                        AllowedCorsOrigins =     { $"https://localhost:4001" },

                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "shoppingAPI"
                        }
                    }
            };
    }
}
