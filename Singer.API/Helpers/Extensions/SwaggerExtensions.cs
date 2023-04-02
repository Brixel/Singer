using System.Linq;
using System.Net;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using NSwag;
using NSwag.Generation.Processors.Security;

namespace Singer.Helpers.Extensions;

public static class SwaggerExtensions
{
    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        // Register the Swagger services
        builder.Services.AddOpenApiDocument(config =>
        {
            config.DocumentName = "OpenAPI 3";
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
        return builder;
    }
}