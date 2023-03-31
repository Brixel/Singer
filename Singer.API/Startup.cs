using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

using Duende.IdentityServer.EntityFramework.DbContexts;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using NSwag;
using NSwag.Generation.Processors.Security;

using Singer.Configuration;
using Singer.Controllers;
using Singer.Data;
using Singer.Data.Models.Configuration;
using Singer.DTOs.Users;
using Singer.Helpers.Extensions;
using Singer.Models.Users;
using Singer.Services;
using Singer.Services.Interfaces;

namespace Singer;

public class Startup
{

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = Configuration.GetSection("ConnectionStrings").GetChildren().Single(x => x.Key == "Application").Value;
        var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception("No connectionString found");
        }

        // This line uses 'UseSqlServer' in the 'options' parameter
        // with the connection string defined above.

        var passwordOptions = Configuration.GetSection("PasswordOptions").Get<PasswordOptions>();
        services
           .AddDbContext<ApplicationDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connectionString,
              opt => opt.EnableRetryOnFailure()))
           .AddIdentity<User, IdentityRole<Guid>>(opts => { opts.Password = passwordOptions; })
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();

        services.AddIdentityServer()
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

        var applicationConfig = Configuration.GetSection("Application").Get<ApplicationConfig>();

        services.AddControllersWithViews();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               // The API resource scope issued in authorization server
               options.TokenValidationParameters.ValidAudience = "singer.api";
               // URL of my authorization server
               options.Authority = applicationConfig.Authority;
           });

        // Making JWT authentication scheme the default
        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
        });

        // In production, the Angular files will be served from this directory
        services.AddSpaStaticFiles(configuration =>
        {
            configuration.RootPath = "ClientApp/dist";
        });

        // Register the Swagger services
        services.AddOpenApiDocument(config =>
        {
            config.DocumentName = "OpenAPI 3";
            config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
            //config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT Token")); -> Replaced the line above with this with no difference
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

        services.AddSwaggerDocument(config =>
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
        services.AddAutoMapper(typeof(Startup));
        services.AddScoped<ICareUserService, CareUserService>();
        services.AddScoped<ILegalGuardianUserService, LegalGuardianUserService>();
        services.AddScoped<ISingerLocationService, SingerLocationService>();
        services.AddScoped<IAdminUserService, AdminUserService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IEventRegistrationService, EventRegistrationService>()
           .AddScoped<IDateValidator, DateValidator>();
        services.AddScoped<IActionNotificationService, ActionNotificationService>();
        services.Configure<PasswordOptions>(Configuration.GetSection("PasswordOptions"));
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.Configure<ApplicationConfig>(Configuration.GetSection("Application"));
        services.AddScoped<IRegistrationService, RegistrationService>();

        var emailOptions = new EmailOptions();
        Configuration.Bind(EmailOptions.SECTION_NAME, emailOptions);
        services.Configure<EmailOptions>(Configuration.GetSection(EmailOptions.SECTION_NAME));
        if (emailOptions.IsValid)
        {
            services.AddScoped(typeof(IEmailService<LegalGuardianUserDTO>),
               typeof(EmailService<LegalGuardianUserDTO>));
            services.AddScoped(typeof(IEmailService<AdminUserDTO>),
               typeof(EmailService<AdminUserDTO>));
            services.AddScoped(typeof(IEmailService<UserDTO>),
               typeof(EmailService<UserDTO>));
            services.AddScoped(typeof(IEmailService),
               typeof(EmailService));
        }
        else
        {
            services.AddScoped(typeof(IEmailService<LegalGuardianUserDTO>),
               typeof(NoActualEmailService<LegalGuardianUserDTO>));
            services.AddScoped(typeof(IEmailService<AdminUserDTO>),
               typeof(NoActualEmailService<AdminUserDTO>));
            services.AddScoped(typeof(IEmailService<UserDTO>),
               typeof(NoActualEmailService<UserDTO>));
            services.AddScoped(typeof(IEmailService),
               typeof(NoActualEmailService));
        }
        var instrumentationKey = Configuration.GetSection("ApplicationInsights").GetChildren().SingleOrDefault(x => x.Value == "InstrumentationKey")?.Value;
        services.AddApplicationInsightsTelemetry(instrumentationKey);
    }

    private static IIdentityServerBuilder AddIdentityService(IServiceCollection services, X509Certificate2 cert)
    {
        return services.AddIdentityServer()
                    .AddSigningCredential(cert);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        MigrateContexts(app);

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();

        app.UseStaticFiles();
        app.UseSpaStaticFiles();

        app.UseExceptionMiddleware();
        app.UseIdentityServer();


        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

        // Register the Swagger generator and the Swagger UI middlewares
        // Navigate to:
        // https://localhost:5001/swagger/index.html the Swagger UI.
        // https://localhost:5001/swagger/v1/swagger.json to view the Swagger specification.
        app.UseOpenApi();
        app.UseSwaggerUi3();

        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");
        });

        app.UseSpa(spa =>
        {
            // To learn more about options for serving an Angular SPA from ASP.NET Core,
            // see https://go.microsoft.com/fwlink/?linkid=864501

            spa.Options.SourcePath = "ClientApp";

            if (env.IsDevelopment())
            {
                spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
            }
        });
    }

    private void MigrateContexts(IApplicationBuilder app)
    {
        var initialAdminPassword = Configuration.GetSection("Application").Get<ApplicationConfig>().InitialAdminUserPassword;
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

        var configrationDbContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        configrationDbContext.Database.Migrate();

        var persistedGrantDbContext = serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
        persistedGrantDbContext.Database.Migrate();

        var applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        applicationDbContext.Database.Migrate();

        Seed.SeedRoles(serviceScope);
        Seed.SeedUsers(serviceScope, applicationDbContext, initialAdminPassword);
        Seed.CheckRoles(serviceScope, applicationDbContext).Wait();
        Seed.CreateAPIAndClient(configrationDbContext);
        Seed.SeedIdentityResources(configrationDbContext);
        Seed.SeedSingerLocations(applicationDbContext);
        configrationDbContext.SaveChanges();
        applicationDbContext.SaveChanges();
    }
}
