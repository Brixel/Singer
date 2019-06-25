using System;

namespace Singer.Helpers.Exceptions
{
   public abstract class ServerException : HttpException
   {
      protected ServerException()
      {
      }

      protected ServerException(string message)
         : base(message)
      {
      }

      protected ServerException(string message, Exception innerException)
         : base(message, innerException)
      {
      }
   }
}
