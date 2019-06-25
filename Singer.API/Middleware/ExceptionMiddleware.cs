using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Singer.Helpers.Exceptions;

namespace Singer.Middleware
{
   public class ExceptionMiddleware
   {
      private readonly RequestDelegate _next;
      private readonly IHostingEnvironment _env;

      public ExceptionMiddleware(RequestDelegate next, IHostingEnvironment env)
      {
         _next = next;
         _env = env;
      }

      public async Task InvokeAsync(HttpContext context)
      {
         try
         {
            await _next(context);
         }
         catch (HttpException e)
         {
            context.Response.StatusCode = e.StatusCode;
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            await context.Response.WriteAsync(e.ClientMessage);

            if (_env.IsDevelopment())
               await context.Response.WriteAsync($"\r\n\r\n{e.Message}");

         }
         catch (Exception e)
         {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync("Whut?");

            if (_env.IsDevelopment())
               await context.Response.WriteAsync($"\r\n\r\n{e.Message}");
         }
      }
   }
}
