using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            new List<ApiScope>
            {
            new ApiScope("ShoppingAPI","Shopping API Scope")
            };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "Client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("demoSecret".Sha256())
                },

                RedirectUris = { "https://localhost:44343/signin-oidc" },

                PostLogoutRedirectUris = { "https://localhost:44343/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "ShoppingAPI"
                }
            }
        };
    }
}
