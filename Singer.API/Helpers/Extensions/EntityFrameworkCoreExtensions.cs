using System;
using System.Threading.Tasks;

using Duende.IdentityServer.EntityFramework.DbContexts;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Singer.Configuration;
using Singer.Data;
using Singer.Data.Models.Configuration;

namespace Singer.Helpers.Extensions;

public static class EntityFrameworkCoreExtensions
{
    public static WebApplicationBuilder AddApplicationDbContext(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("Application");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception("No connectionString found");
        }

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseLazyLoadingProxies().UseSqlServer(connectionString, opt => opt.EnableRetryOnFailure()));

        return builder;
    }

    public static async Task<WebApplication> MigrateContexts(this WebApplication app)
    {
        var initialAdminPassword = app.Configuration.GetSection("Application").Get<ApplicationConfig>().InitialAdminUserPassword;
        using var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope();

        var configrationDbContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        configrationDbContext.Database.Migrate();

        var persistedGrantDbContext = serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
        persistedGrantDbContext.Database.Migrate();

        var applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        applicationDbContext.Database.Migrate();

        await Seed.SeedRolesAsync(serviceScope);
        await Seed.SeedUsersAsync(serviceScope, applicationDbContext, initialAdminPassword);
        await Seed.CheckRolesAsync(serviceScope, applicationDbContext);
        Seed.CreateAPIAndClient(configrationDbContext);
        Seed.SeedIdentityResources(configrationDbContext);
        Seed.SeedSingerLocations(applicationDbContext);
        await configrationDbContext.SaveChangesAsync();
        await applicationDbContext.SaveChangesAsync();
        return app;
    }
}