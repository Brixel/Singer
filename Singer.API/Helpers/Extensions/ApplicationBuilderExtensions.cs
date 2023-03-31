using Microsoft.AspNetCore.Builder;

using Singer.Middleware;

namespace Singer.Helpers.Extensions;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Extension method that adds the <see cref="ExceptionMiddleware"/> to the current <see cref="IApplicationBuilder"/>.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
       => builder.UseMiddleware<ExceptionMiddleware>();
}
