using System.Linq;
using System.Net;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NSwag;
using NSwag.Generation.Processors.Security;

using Singer.Data.Models.Configuration;
using Singer.DTOs.Users;
using Singer.Profiles;
using Singer.Services;
using Singer.Services.Interfaces;

namespace Singer.Helpers.Extensions;

public static class ServiceExtensions
{
    public static WebApplicationBuilder AddSingerServices(this WebApplicationBuilder builder)
    {

        builder.Services.AddSwaggerDocument(config =>
        {
            config.DocumentName = "OpenAPI 2";
            config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
            config.AddSecurity("JWT Token", Enumerable.Empty<string>(),
            new OpenApiSecurityScheme()
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = nameof(Authorization),
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Copy this into the value field: Bearer {token}"
            }
         );
        });

        // Adds AutoMapper. Maps are defined as profiles in ./Profiles/*Profile.cs
        builder.Services.AddAutoMapper(typeof(EventProfile));
        builder.Services.AddScoped<ISingerLocationService, SingerLocationService>();
        builder.Services.AddScoped<IEventService, EventService>();
        builder.Services.AddScoped<IEventRegistrationService, EventRegistrationService>()
           .AddScoped<IDateValidator, DateValidator>();
        builder.Services.AddScoped<IActionNotificationService, ActionNotificationService>();
        builder.Services.Configure<PasswordOptions>(builder.Configuration.GetSection("PasswordOptions"));

        builder.Services.Configure<ApplicationConfig>(builder.Configuration.GetSection("Application"));
        builder.Services.AddScoped<IRegistrationService, RegistrationService>();

        var emailOptions = new EmailOptions();
        builder.Configuration.Bind(EmailOptions.SECTION_NAME, emailOptions);
        builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(EmailOptions.SECTION_NAME));
        if (emailOptions.IsValid)
        {
            builder.Services.AddScoped(typeof(IEmailService<LegalGuardianUserDTO>),
               typeof(EmailService<LegalGuardianUserDTO>));
            builder.Services.AddScoped(typeof(IEmailService<AdminUserDTO>),
               typeof(EmailService<AdminUserDTO>));
            builder.Services.AddScoped(typeof(IEmailService<UserDTO>),
               typeof(EmailService<UserDTO>));
            builder.Services.AddScoped(typeof(IEmailService),
               typeof(EmailService));
        }
        else
        {
            builder.Services.AddScoped(typeof(IEmailService<LegalGuardianUserDTO>),
               typeof(NoActualEmailService<LegalGuardianUserDTO>));
            builder.Services.AddScoped(typeof(IEmailService<AdminUserDTO>),
               typeof(NoActualEmailService<AdminUserDTO>));
            builder.Services.AddScoped(typeof(IEmailService<UserDTO>),
               typeof(NoActualEmailService<UserDTO>));
            builder.Services.AddScoped(typeof(IEmailService),
               typeof(NoActualEmailService));
        }
        return builder;
    }
}