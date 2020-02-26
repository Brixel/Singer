using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Singer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Singer.Models.Users;
using ApiResource = IdentityServer4.EntityFramework.Entities.ApiResource;
using IdentityResource = IdentityServer4.EntityFramework.Entities.IdentityResource;
using Singer.Identity;
using Singer.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Client = IdentityServer4.EntityFramework.Entities.Client;

namespace Singer.Configuration
{
   public static class Seed
   {

      private static List<EventLocation> _eventLocations = new List<EventLocation>() {
         new EventLocation{
            Name="Brixel",
            Address= "Spalbeekstraat",
            City="Spalbeek",
            Country="Belgie",
            PostalCode="3510"
         },
         new EventLocation{
            Name="Sint-Gerardus",
            Address= "Sint-Gerardusdreef 1",
            City="Diepenbeek",
            Country="Belgie",
            PostalCode="3590"
         }
      };

      public static void SeedUsers(IServiceScope serviceScope, ApplicationDbContext applicationDbContext, string initialAdminPassword)
      {
         var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
         var roleMgr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
         var admin = userMgr.FindByNameAsync("admin").Result;
         var adminUsersInDatabase = applicationDbContext.AdminUsers.Any();
         if (!adminUsersInDatabase)
         {
            if (admin == null)
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

               if (!result.Succeeded)
               {
                  throw new Exception(result.Errors.First().Description);
               }
            }

            var adminUser = new AdminUser()
            {
               User = admin,
               UserId = admin.Id
            };

            applicationDbContext.AdminUsers.Add(adminUser);

            Console.WriteLine("admin created");
            var _ = userMgr.AddToRoleAsync(admin, Roles.ROLE_ADMINISTRATOR).Result;
            _ = userMgr.AddClaimAsync(admin, new Claim(ClaimTypes.Role, Roles.ROLE_ADMINISTRATOR)).Result;

         }
         else
         {
            Console.WriteLine("admin already exists");
            if (admin != null)
            {
               var hasRole = userMgr.IsInRoleAsync(admin, Roles.ROLE_ADMINISTRATOR).Result;
               if (!hasRole)
               {
                  var _ = userMgr.AddToRoleAsync(admin, Roles.ROLE_ADMINISTRATOR).Result;
               }
            }

         }

         foreach (var careUser in Roles._careUsers)
         {
            var user = userMgr.FindByNameAsync(careUser).Result;
            if (user == null)
            {
               user = new User()
               {
                  UserName = careUser,
                  LastName = careUser
               };

               var __ = userMgr.CreateAsync(user).Result;
               if (!__.Succeeded)
               {
                  throw new Exception(__.Errors.First().Description);

               }
               var roleTask = userMgr.AddToRoleAsync(user, Roles.ROLE_CAREUSER).Result;
               if (!roleTask.Succeeded)
               {
                  throw new Exception(roleTask.Errors.First().Description);
               }
               var careuser = new CareUser()
               {
                  User = user,
                  AgeGroup = AgeGroup.Child,
                  CaseNumber = new Random().Next(1000, 5000).ToString(),
                  BirthDay = DateTime.UtcNow.AddYears(new Random().Next(-14, -5)),
                  HasTrajectory = false,
                  IsExtern = false,
                  UserId = user.Id
               };

               applicationDbContext.CareUsers.Add(careuser);
            }
         }
      }

      public static async Task CheckRoles(IServiceScope serviceScope, ApplicationDbContext applicationDbContext)
      {
         var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
         var roleMgr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
         foreach (var lgUser in applicationDbContext.LegalGuardianUsers.Include(x => x.User))
         {
            var hasRole = await userMgr.IsInRoleAsync(lgUser.User, Roles.ROLE_LEGALGUARDIANUSER);
            if (!hasRole)
            {
               var roleTask = userMgr.AddToRoleAsync(lgUser.User, Roles.ROLE_LEGALGUARDIANUSER).Result;
               if (!roleTask.Succeeded)
               {
                  throw new Exception(roleTask.Errors.First().Description);
               }
            }
         }
      }

      public static void SeedRoles(IServiceScope serviceScope, ApplicationDbContext applicationDbContext)
      {
         foreach (var role in Roles.ROLES)
         {

            var roleMgr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var roleExists = roleMgr.RoleExistsAsync(role).Result;
            if (!roleExists)
            {
               var _ = roleMgr.CreateAsync(new IdentityRole<Guid>(role)).Result;
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

      public static void SeedIdentityResources(ConfigurationDbContext configrationDbContext)
      {
         var identityResources = new List<IdentityResource>
         {
            new IdentityResources.OpenId().ToEntity(),
            new IdentityResources.Email().ToEntity(),
            new IdentityResources.Profile().ToEntity(),
            new IdentityResources.Phone().ToEntity(),
            new IdentityResources.Address().ToEntity(),
            new IdentityResource(){
               Name = "Role",
               UserClaims = new List<IdentityClaim>()
            {
               new IdentityClaim()
               {
                  Type = JwtClaimTypes.Role
               }
            }}
         };


         foreach (var identityResource in identityResources)
         {
            var identityResourceInDb = configrationDbContext.IdentityResources.SingleOrDefault(x => x.Name == identityResource.Name);
            if (identityResourceInDb == null)
            {
               configrationDbContext.IdentityResources.Add(identityResource);
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
                     Required = true,
                     UserClaims = new List<ApiScopeClaim>()
                     {
                        new ApiScopeClaim()
                        {
                           Type = ClaimTypes.Role,
                        },
                        new ApiScopeClaim()
                        {
                           Type = ClaimTypes.Name
                        }
                     }
                  }
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
                  },

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

         UpdateAccessTokenLifeTime(singerApiClient);
      }

      private static void UpdateAccessTokenLifeTime(Client singerApiClient) =>
         singerApiClient.AccessTokenLifetime = 3600 * 24;

      public static void SeedEventLocations(ApplicationDbContext applicationDbContext)
      {
         if (applicationDbContext.EventLocations.Count() == 0)
         {
            foreach (var loc in _eventLocations)
            {
               applicationDbContext.Add(loc);
            }
         }
      }
   }
}
