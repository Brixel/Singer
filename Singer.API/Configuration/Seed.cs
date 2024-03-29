﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Mappers;

using IdentityModel;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Singer.Data;
using Singer.Identity;
using Singer.Models;
using Singer.Models.Users;

namespace Singer.Configuration;

public static class Seed
{

    private static readonly List<SingerLocation> _eventLocations = new() {
     new SingerLocation{
        Name="Hasselt",
        Address= "Ekkelgaarden 20",
        City="Hasselt",
        Country="Belgie",
        PostalCode="3500"
     },
     new SingerLocation{
        Name="Diepenbeek",
        Address= "Sint-Gerardusdreef 1",
        City="Diepenbeek",
        Country="Belgie",
        PostalCode="3590"
     }
  };

    public static async Task SeedUsersAsync(IServiceScope serviceScope, ApplicationDbContext applicationDbContext, string initialAdminPassword)
    {
        var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleMgr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var admin = await userMgr.FindByNameAsync("admin");
        var adminUsersInDatabase = applicationDbContext.AdminUsers.Any();
        if (!adminUsersInDatabase)
        {
            if (admin == null)
            {
                admin = new User
                {
                    UserName = "admin"
                };
                var result = await userMgr.CreateAsync(admin, initialAdminPassword);
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
            await userMgr.AddToRoleAsync(admin, Roles.ROLE_ADMINISTRATOR);
            await userMgr.AddClaimAsync(admin, new Claim(ClaimTypes.Role, Roles.ROLE_ADMINISTRATOR));
        }
        else
        {
            Console.WriteLine("admin already exists");
            if (admin != null)
            {
                var hasRole = await userMgr.IsInRoleAsync(admin, Roles.ROLE_ADMINISTRATOR);
                if (!hasRole)
                {
                    await userMgr.AddToRoleAsync(admin, Roles.ROLE_ADMINISTRATOR);
                }
            }

        }

        foreach (var careUser in Roles.CareUsers)
        {
            var user = await userMgr.FindByNameAsync(careUser);
            if (user == null)
            {
                user = new User()
                {
                    UserName = careUser,
                    LastName = careUser
                };

                var createUserTask = await userMgr.CreateAsync(user);
                if (!createUserTask.Succeeded)
                {
                    throw new Exception(createUserTask.Errors.First().Description);

                }
                var roleTask = await userMgr.AddToRoleAsync(user, Roles.ROLE_CAREUSER);
                if (!roleTask.Succeeded)
                {
                    throw new Exception(roleTask.Errors.First().Description);
                }
                var careuser = new CareUser()
                {
                    User = user,
                    AgeGroup = AgeGroup.Child,
                    BirthDay = DateTime.UtcNow.AddYears(new Random().Next(-14, -5)),
                    HasTrajectory = false,
                    IsExtern = false,
                    UserId = user.Id
                };

                applicationDbContext.CareUsers.Add(careuser);
            }
        }
    }

    public static async Task CheckRolesAsync(IServiceScope serviceScope, ApplicationDbContext applicationDbContext)
    {
        var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleMgr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        foreach (var lgUser in applicationDbContext.LegalGuardianUsers.Include(x => x.User))
        {
            var hasRole = await userMgr.IsInRoleAsync(lgUser.User, Roles.ROLE_LEGALGUARDIANUSER);
            if (!hasRole)
            {
                var roleTask = await userMgr.AddToRoleAsync(lgUser.User, Roles.ROLE_LEGALGUARDIANUSER);
                if (!roleTask.Succeeded)
                {
                    throw new Exception(roleTask.Errors.First().Description);
                }
            }
        }
    }

    public static async Task SeedRolesAsync(IServiceScope serviceScope)
    {
        foreach (var role in Roles.ROLES)
        {

            var roleMgr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var roleExists = await roleMgr.RoleExistsAsync(role);
            if (!roleExists)
            {
                var createRoleTask = await roleMgr.CreateAsync(new IdentityRole<Guid>(role));
                if (createRoleTask.Succeeded)
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
        new Duende.IdentityServer.Models.IdentityResources.OpenId().ToEntity(),
        new Duende.IdentityServer.Models.IdentityResources.Email().ToEntity(),
        new Duende.IdentityServer.Models.IdentityResources.Profile().ToEntity(),
        new Duende.IdentityServer.Models.IdentityResources.Phone().ToEntity(),
        new Duende.IdentityServer.Models.IdentityResources.Address().ToEntity(),
        new IdentityResource{
           Name = "Role",
           UserClaims = new List<IdentityResourceClaim>()
        {
           new IdentityResourceClaim()
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
                ShowInDiscoveryDocument = true,
                Scopes = new()
                {
                    new ()
                    {
                        Scope = "apiRead",
                        ApiResource = new()
                        {
                            Name = "apiRead",
                            ShowInDiscoveryDocument = true,
                            DisplayName = "Readonly scope for SingerAPI",
                            UserClaims = new ()
                            {
                                new ()
                                {
                                    Type = ClaimTypes.Role,
                                },
                                new ()
                                {
                                    Type = ClaimTypes.Name
                                }
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

    public static void SeedSingerLocations(ApplicationDbContext applicationDbContext)
    {
        if (!applicationDbContext.SingerLocations.Any())
        {
            foreach (var loc in _eventLocations)
            {
                applicationDbContext.Add(loc);
            }
        }
    }
}
