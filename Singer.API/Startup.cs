using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using IdentityModel;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Singer.Configuration;
using Singer.Data;
using Singer.Data.Identity;
using Singer.Data.Models;
using Singer.Data.Models.Configuration;
using Singer.IdentityService;
using Singer.Models;
using Singer.Services;
using Singer.Services.Utils;
using ApiResource = IdentityServer4.EntityFramework.Entities.ApiResource;

namespace Singer
{
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

         // This line uses 'UseSqlServer' in the 'options' parameter
         // with the connection string defined above.
         services
            .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString))
            .AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

         var applicationConfig = Configuration.GetSection("Application").Get<ApplicationConfig>();
         var certFileName = applicationConfig.CertFileName;
         var certThumbprint = applicationConfig.CertThumbprint;
         var certPassword = applicationConfig.CertPass;

         X509Certificate2 cert;
         if (!string.IsNullOrEmpty(certThumbprint))
         {
            cert = CertificateService.LoadCert(certThumbprint);
         }
         else
         {
            cert = CertificateService.LoadCert(certFileName, certPassword);
         }

         if (cert == null)
         {
            throw new ArgumentNullException("Not able to load certificate");
         }

         services.AddIdentityServer()

            .AddSigningCredential(cert)
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
            //.AddResourceOwnerValidator<ResourceOwnerPasswordValidator<User>>();

         var authority = applicationConfig.Authority;

         services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
         services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {

               // The API resource scope issued in authorization server
               options.ApiName = "singer.api";
               // URL of my authorization server
               options.Authority = authority;
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
         services.AddSwaggerDocument();
      }

      private static IIdentityServerBuilder AddIdentityService(IServiceCollection services, X509Certificate2 cert)
      {
         return services.AddIdentityServer()
                     .AddSigningCredential(cert);
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

         app.UseIdentityServer();


         app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

         // Register the Swagger generator and the Swagger UI middlewares
         // Navigate to:
         // https://localhost:5001/swagger/index.html the Swagger UI.
         // https://localhost:5001/swagger/v1/swagger.json to view the Swagger specification.
         app.UseSwagger();
         app.UseSwaggerUi3();

         app.UseMvc(routes =>
         {
            routes.MapRoute(
                   name: "default",
                   template: "{controller}/{action=Index}/{id?}");
         });

         app.UseSpa(spa =>
         {
            // To learn more about options for serving an Angular SPA from ASP.NET Core,
            // see https://go.microsoft.com/fwlink/?linkid=864501

            spa.Options.SourcePath = "ClientApp";

            if (env.IsDevelopment())
            {
               spa.UseAngularCliServer(npmScript: "start");
            }
         });
      }

      private void MigrateContexts(IApplicationBuilder app)
      {
         var initialAdminPassword = Configuration.GetSection("Application").Get<ApplicationConfig>().InitialAdminUserPassword;
         using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
         {
            var applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            applicationDbContext.Database.Migrate();

            var configrationDbContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            configrationDbContext.Database.Migrate();

            var persistedGrantDbContext = serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
            persistedGrantDbContext.Database.Migrate();
                       
            Seed.SeedRoles(serviceScope, applicationDbContext);
            Seed.SeedUsers(serviceScope, applicationDbContext, initialAdminPassword);
            Seed.CreateAPIAndClient(configrationDbContext);
            Seed.SeedIdentityResources(configrationDbContext);
         }
      }
   }
}
