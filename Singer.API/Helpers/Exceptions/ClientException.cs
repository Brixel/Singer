using System;

namespace Singer.Helpers.Exceptions
{
   public abstract class ClientException : HttpException
   {
      protected ClientException()
      {
      }

      protected ClientException(string message)
         : base(message)
      {
      }

      protected ClientException(string message, Exception innerException)
         : base(message, innerException)
      {
      }
   }
}
