using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.Extensions.DependencyInjection;
using Singer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Singer.Configuration
{
   public static class Seed
   {
      private const string ROLE_ADMINISTRATOR = "Administrator";
      private const string ROLE_SOCIALSERVICES = "SocialServices";
      private const string ROLE_CARETAKER = "Caretaker";
      private const string ROLE_CAREUSER = "CareUser";
      private List<string> ROLES = new List<string>()
      {
         ROLE_ADMINISTRATOR,
         ROLE_SOCIALSERVICES,
         ROLE_CARETAKER,
         ROLE_CAREUSER
      };
      public static void SeedUsers(IServiceScope serviceScope, ApplicationDbContext applicationDbContext)
      {
         var initialAdminPassword = Configuration.GetSection("Application").GetChildren().Single(x => x.Key == "InitialAdminUserPassword").Value;
         var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
         var roleMgr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
         var admin = userMgr.FindByNameAsync("admin").Result;
         var usersInDatabase = applicationDbContext.Users.Any();
         if (admin == null && !usersInDatabase)
         {
            admin = new User
            {
               UserName = "admin"
            };
            var result = userMgr.CreateAsync(admin, initialAdminPassword).Result;
            if (!result.Succeeded)
            {
               throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(admin, new Claim[]{
               new Claim(JwtClaimTypes.Name, "Admin"),
               new Claim(JwtClaimTypes.GivenName, "GivenName"),
               new Claim(JwtClaimTypes.FamilyName, "FamilyName"),
               new Claim(JwtClaimTypes.Email, "email@host.example"),
               new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
               new Claim(JwtClaimTypes.WebSite, "http://host.example"),
               new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
            }).Result;
            if (!result.Succeeded)
            {
               throw new Exception(result.Errors.First().Description);
            }
            Console.WriteLine("admin created");
         }
         else
         {
            Console.WriteLine("admin already exists");
         }
         var _ = userMgr.AddToRoleAsync(admin, ROLE_ADMINISTRATOR).Result;
      }

      public static void SeedRoles(IServiceScope serviceScope, ApplicationDbContext applicationDbContext)
      {
         foreach (var role in ROLES)
         {

            var roleMgr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roleExists = roleMgr.RoleExistsAsync(role).Result;
            if (!roleExists)
            {
               var _ = roleMgr.CreateAsync(new IdentityRole(role)).Result;
               if (_.Succeeded)
               {
                  Console.WriteLine($"Added role {role}");

               }
               else
               {
                  Console.WriteLine($"Failed to add role {role}");
               }
            }
         }
      }

      public static void CreateAPIAndClient(ConfigurationDbContext configrationDbContext)
      {
         var singerApiResourceName = "singer.api";
         var apiResource = configrationDbContext.ApiResources.SingleOrDefault(x => x.Name == singerApiResourceName);
         if (apiResource == null)
         {
            apiResource = new ApiResource()
            {
               Name = singerApiResourceName,
               Scopes = new List<ApiScope>()
               {
                  new ApiScope()
                  {
                     Name = "apiRead",
                     DisplayName = "Readonly scope for SingerAPI",
                     Required = true
                  },
               },

               UserClaims = new List<ApiResourceClaim>()
               {
                  new ApiResourceClaim()
                  {
                     Type = ClaimTypes.Name,
                  },
                  new ApiResourceClaim()
                  {
                     Type = ClaimTypes.Email
                  }
               }
            };


            configrationDbContext.ApiResources.Add(apiResource);
         }


         var clientId = "singer.client";
         var singerApiClient = configrationDbContext.Clients.SingleOrDefault(x => x.ClientId == clientId);
         if (singerApiClient == null)
         {
            singerApiClient = Config.GetClient().ToEntity();
            configrationDbContext.Clients.Add(singerApiClient);
         }

         configrationDbContext.SaveChanges();
      }
   }
}
