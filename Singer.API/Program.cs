using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

using Singer.Helpers.Extensions;


try
{

    var builder = WebApplication.CreateBuilder(args);

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}")
        .CreateLogger();

    builder.Host.UseSerilog();

    builder
        .AddAuthenticationAndAuthorization()
        .AddAngularSpa()
        .AddApplicationInsights()
        .AddApplicationDbContext()
        .AddSingerServices()
        .AddSwagger();

    builder.Services
        .AddControllersWithViews();
    Log.Information("Building application");
    var app = builder.Build();

    Log.Information("Starting up...");
    await app.MigrateContexts();
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }


    app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();

    app.UseStaticFiles();
    app.UseSpaStaticFiles();

    app.UseExceptionMiddleware();
    //app.UseIdentityServer();

    // Register the Swagger generator and the Swagger UI middlewares
    // Navigate to:
    // https://localhost:5001/swagger/index.html the Swagger UI.
    // https://localhost:5001/swagger/v1/swagger.json to view the Swagger specification.
    app.UseOpenApi();
    app.UseSwaggerUi3();

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

        if (app.Environment.IsDevelopment())
        {
            spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
        }
    });

    await app.RunAsync();
}
catch (Exception e)
{
    Log.Fatal(e, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}