# Singer

Inschrijvingssysteem voor Sint Gerardus

# Setup (English only)

## Code

The application is written in C#, using .NET Core 2.2 and Angular (?). It's is generated using a template provided by Visual Studio 2017.
This template allows running the entire application (both the API and the webapp) in one executable.
The magic is done in the `Startup.cs` file, especially the following part:

```C#
services.AddSpaStaticFiles(configuration =>
{
   configuration.RootPath = "ClientApp/dist";
});
```

and

```C#
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
```

The `spa.UseAngularCliServer` line triggers the `npm start` for the webapp and starts a browser opening the page.

## Get the application running locally

1. Clone this repository
2. Run `docker-compose up` in the root of the cloned folder
2. Run the application by hitting F5 in Visual Studio
3. Enjoy the magic 


## Docker

The application requires a MSSQL database. You can run it on your existing databaseserver or use the provided `docker-compose.yml` file to run a linux based MSSQL Server (it's using mcr.microsoft.com/mssql/server). Note that it also includes a mapped volume from the container's /var/opt/mssql/data to a folder in the root of the project.
