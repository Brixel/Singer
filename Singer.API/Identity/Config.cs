using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace Singer.Identity
{
   public static class Config
   {
      public static Client GetClient()
      {
         return new Client
         {
            ClientId = "singer.client",
            ClientName = "Singer JS Client",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            RequirePkce = true,
            RequireClientSecret = true,
            ClientSecrets = new List<Secret>()
            {
               new Secret("secret".ToSha256())
            },
            RedirectUris = {"https://localhost:5001/index.html"},
            PostLogoutRedirectUris = {"https://localhost:5001/index.html"},
            AllowedCorsOrigins = {"https://localhost:5001"},

            AllowedScopes =
            {
               "apiRead",
               OidcConstants.StandardScopes.OpenId,
               OidcConstants.StandardScopes.Email,
               OidcConstants.StandardScopes.Profile,
               "Role"
            }
         };
      }
   }
}
