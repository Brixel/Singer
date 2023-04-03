using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Singer.Helpers.Extensions;

public static class SpaExtensions
{
    public static WebApplicationBuilder AddAngularSpa(this WebApplicationBuilder builder)
    {
        builder.Services.AddSpaStaticFiles(configuration =>
        {
            configuration.RootPath = "ClientApp/dist";
        });

        return builder;
    }
}