using System;
using System.Threading.Tasks;

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
        using var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope();

        var applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await applicationDbContext.Database.MigrateAsync();

        Seed.SeedSingerLocations(applicationDbContext);
        
        await applicationDbContext.SaveChangesAsync();
        return app;
    }
}