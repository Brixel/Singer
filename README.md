# Singer

[English below]

Inschrijvingssysteem voor Sint Gerardus

## Functionaliteit

### Gebruikersbeheer

* Beheren van verschillende types gebruikers:
  * Administrators
  * Zorggebruikers
  * Voogden
  
### Evenementenbeheer

* Beheren van evenementen
  * Voogen/ouders van zorggebruikers kunnen hun kind inschrijven voor evenementen
  * Verschillende leeftijdsgroepen toekennen aan evenementen
  * Evenementen kunnen over één of meerdere dagen plaatsvinden

## Setup (English only)

### Code

1. Architecture

The application is written in C#, using .NET Core 2.2 and Angular 8. It's is generated using a template provided by Visual Studio 2017.
This template allows running the entire application (both the API and the webapp) in one executable.
The data is stored in a SQL Server instance. Accessing the data is done via Entity Framework Core, using Automapper.

2. Angular template in Visual Studio

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

## Hosting and CI

For development and demo purposes, we host our application in Azure. Using Azure DevOps we can quickly deploy the application to Azure, while it allows us to have finegrained control over what is being deployed to Sint Gerardus.
