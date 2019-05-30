using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using static IdentityModel.OidcConstants;

namespace Singer.Data.Identity
{
   public static class Config
   {
      public static Client GetClient()
      {
         return new Client
         {
            ClientId = "singer.client",
            ClientName = "Singer JS Client",
            AllowedGrantTypes = IdentityServer4.Models.GrantTypes.ResourceOwnerPassword,
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
               StandardScopes.OpenId,
               StandardScopes.Email,
               StandardScopes.Profile
            }
         };
      }
   }
}
