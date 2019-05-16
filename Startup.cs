using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Singer.Data;

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
         var connection = Configuration.GetSection("ConnectionStrings").GetChildren().Single(x => x.Key == "Application").Value;

         // This line uses 'UseSqlServer' in the 'options' parameter
         // with the connection string defined above.
         services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(connection));

         services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddInMemoryApiResources(new List<ApiResource>
            {
               new ApiResource("resource.server.api",
                  new [] { ClaimTypes.Name, ClaimTypes.Email})
            })
            .AddInMemoryClients(new List<Client>
            {
               new Client
               {
                  ClientId = "angular.client",
                  ClientName = "Angular Client",
                  ClientSecrets = new [] { new Secret("secret".Sha256())  },
                  AllowedScopes = new [] { "resource.server.api" },
                  AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                  AllowedCorsOrigins = new [] { "http://localhost:4200/" }
               }
            })
            .AddTestUsers(new List<TestUser>
            {
               new TestUser
               {
                  SubjectId = "1",
                  Username = "user",
                  Password = "1234",
                  Claims = new List<Claim>
                  {
                     new Claim(ClaimTypes.Name, "Test User"),
                     new Claim(ClaimTypes.Email, "email@mail.com")
                  }
               }
            });

         services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
         services.AddAuthentication()
            .AddJwtBearer(options =>
            {
               // The API resource scope issued in authorization server
               options.Audience = "resource.server.api";
               // URL of my authorization server
               options.Authority = "https://localhost:5001";
            });

         // Making JWT authentication scheme the default
         services.AddAuthorization(options =>
         {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
               .RequireAuthenticatedUser()
               .Build();
         });


         // In production, the Angular files will be served from this directory
         //services.AddSpaStaticFiles(configuration =>
         //{
         //   configuration.RootPath = "ClientApp/dist";
         //});

         // Register the Swagger services
         services.AddSwaggerDocument();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
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
         //app.UseSpaStaticFiles();

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

         //app.UseSpa(spa =>
         //{
         //   // To learn more about options for serving an Angular SPA from ASP.NET Core,
         //   // see https://go.microsoft.com/fwlink/?linkid=864501

         //   spa.Options.SourcePath = "ClientApp";
            
         //   if (env.IsDevelopment())
         //   {
         //      //spa.UseAngularCliServer(npmScript: "start");
         //   }
         //});
      }
   }
}
