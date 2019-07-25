using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Singer.Data;
using Singer.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Singer.Models.Users;
using ApiResource = IdentityServer4.EntityFramework.Entities.ApiResource;
using ClaimValueTypes = System.Security.Claims.ClaimValueTypes;
using IdentityResource = IdentityServer4.EntityFramework.Entities.IdentityResource;
using Singer.Services;
using Singer.DTOs.Users;
using Singer.Models;

namespace Singer.Configuration
{
   public static class Seed
   {
      private const string ROLE_ADMINISTRATOR = "Administrator";
      private const string ROLE_SOCIALSERVICES = "SocialServices";
      private const string ROLE_CARETAKER = "Caretaker";
      private const string ROLE_CAREUSER = "CareUser";

      private static List<string> _careUsers = new List<string>() { "user1", "user2", "user3" };

      private static List<string> ROLES = new List<string>()
      {
         ROLE_ADMINISTRATOR,
         ROLE_SOCIALSERVICES,
         ROLE_CARETAKER,
         ROLE_CAREUSER
      };
      public static void SeedUsers(IServiceScope serviceScope, ApplicationDbContext applicationDbContext, string initialAdminPassword)
      {
         var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
         var roleMgr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
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

         foreach (var careUser in _careUsers)
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
               var roleTask = userMgr.AddToRoleAsync(user, ROLE_CAREUSER).Result;
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
                  HasNormalDayCare = false,
                  HasResources = false,
                  HasTrajectory = false,
                  HasVacationDayCare = false,
                  IsExtern = false,
                  UserId = user.Id
               };

               applicationDbContext.CareUsers.Add(careuser);
            }
         }

         //applicationDbContext.SaveChanges();
      }

      public static void SeedRoles(IServiceScope serviceScope, ApplicationDbContext applicationDbContext)
      {
         foreach (var role in ROLES)
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
            new IdentityResources.Address().ToEntity()
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
                     Required = true
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
      }
   }
}
