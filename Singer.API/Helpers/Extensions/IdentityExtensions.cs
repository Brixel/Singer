using System;
using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Singer.Data;
using Singer.Models.Users;

namespace Singer.Helpers.Extensions;

public static class IdentityExtensions
{
    public static WebApplicationBuilder AddIdentity(this WebApplicationBuilder builder)
    {
        var passwordOptions = builder.Configuration.GetSection("PasswordOptions").Get<PasswordOptions>();
        var connectionString = builder.Configuration.GetConnectionString("Application");
        var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

        builder.Services.AddIdentity<User, IdentityRole<Guid>>(opts => { opts.Password = passwordOptions; })
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();

        builder.Services.AddIdentityServer()
           .AddAspNetIdentity<User>()
           .AddConfigurationStore(options =>
           {
               options.ConfigureDbContext = b =>
               b.UseSqlServer(connectionString,
                  sql => sql.MigrationsAssembly(migrationsAssembly));
           })
           // this adds the operational data from DB (codes, tokens, consents)
           .AddOperationalStore(options =>
           {
               options.ConfigureDbContext = b =>
               b.UseSqlServer(connectionString,
                  sql => sql.MigrationsAssembly(migrationsAssembly));

               // this enables automatic token cleanup. this is optional.
               options.EnableTokenCleanup = true;
           });

        return builder;
    }
}