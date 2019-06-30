using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Singer.Helpers.Exceptions;

namespace Singer.Middleware
{
   /// <summary>
   /// Middle ware that catches all exceptions that thrown.
   /// </summary>
   public class ExceptionMiddleware
   {
      /// <summary>
      /// The next delegate to be invoked.
      /// </summary>
      private readonly RequestDelegate _next;

      /// <summary>
      /// The hosting environment. (Is used to check whether the environment is in development)
      /// </summary>
      private readonly IHostingEnvironment _env;


      /// <summary>
      /// Constructs a new instance of the <see cref="ExceptionMiddleware"/>
      /// </summary>
      /// <param name="next">The next delegate to be invoked.</param>
      /// <param name="env">The hosting environment. (Is used to check whether the environment is in development)</param>
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
