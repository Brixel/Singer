using System.Linq;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Singer.Helpers.Extensions;

public static class ApplicationInsightsExtensions
{
    public static WebApplicationBuilder AddApplicationInsights(this WebApplicationBuilder builder)
    {
        var instrumentationKey = builder.Configuration
            .GetSection("ApplicationInsights")
            .GetChildren()
            .SingleOrDefault(x => x.Value == "InstrumentationKey")?.Value;

        builder.Services.AddApplicationInsightsTelemetry(options =>
        {
            //TODO: We should use connectionstring here instead
            options.InstrumentationKey = instrumentationKey;
        });
        return builder;
    }
}