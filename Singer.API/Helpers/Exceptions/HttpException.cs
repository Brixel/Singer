using System;

namespace Singer.Helpers.Exceptions
{
   public abstract class HttpException : Exception
   {
      protected HttpException()
      {
      }

      protected HttpException(string message)
         : base(message)
      {
      }

      protected HttpException(string message, Exception innerException)
         : base(message, innerException)
      {
      }


      public abstract int StatusCode { get; }
   }
}
