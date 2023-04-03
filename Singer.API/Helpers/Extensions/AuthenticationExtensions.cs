using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Singer.Data.Models.Configuration;

namespace Singer.Helpers.Extensions;

public static class AuthenticationExtensions
{
    public static WebApplicationBuilder AddAuthenticationAndAuthorization(this WebApplicationBuilder builder)
    {
        var applicationConfig = builder.Configuration.GetSection("Application").Get<ApplicationConfig>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               // The API resource scope issued in authorization server
               options.TokenValidationParameters.ValidAudience = "singer.api";
               // URL of my authorization server
               options.Authority = applicationConfig.Authority;
           });

        // Making JWT authentication scheme the default
        builder.Services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
        });

        return builder;
    }
}