using Microsoft.AspNetCore.Builder;
using Singer.Middleware;

namespace Singer.Helpers.Extensions
{
   public static class ApplicationBuilderExtensions
   {
      public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
         => builder.UseMiddleware<ExceptionMiddleware>();
   }
}
