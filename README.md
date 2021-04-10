# Singer

[English below]

Inschrijvingssysteem voor Sint Gerardus

## Functionaliteit

### Gebruikersbeheer

-  Beheren van verschillende types gebruikers:

   -  Administrators
   -  Zorggebruikers
   -  Voogden

### Evenementenbeheer

-  Beheren van evenementen
   -  Voogen/ouders van zorggebruikers kunnen hun kind inschrijven voor evenementen
   -  Verschillende leeftijdsgroepen toekennen aan evenementen
   -  Evenementen kunnen over één of meerdere dagen plaatsvinden

## Setup (English only)

### Code

1. Architecture

The application is written in C#, using .NET Core 3.1 and Angular 11. It's is generated using a template provided by .NET Core.
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
3. Run the application by hitting F5 in Visual Studio
4. Enjoy the magic

Application can be accessed on [https://localhost:5001](https://localhost:5001).  
Email web UI can be accessed on [http://localhost:3000](http://localhost:3000).

## Docker

The application requires a MSSQL database. You can run it on your existing databaseserver or use the provided `docker-compose.yml` file to run a linux based MSSQL Server (it's using mcr.microsoft.com/mssql/server). Note that it also includes a mapped volume from the container's /var/opt/mssql/data to a folder in the root of the project.

Also, a local SMTP server with a web ui ([smtp4dev](https://github.com/rnwood/smtp4dev)) is provided for testing the email functionality of the app.

## Hosting and CI

For development and demo purposes, we host our application in Azure. Using Azure DevOps we can quickly deploy the application to Azure, while it allows us to have finegrained control over what is being deployed to Sint Gerardus.
