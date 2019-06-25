using System;

namespace Singer.Helpers.Exceptions
{
   public class ForbiddenException : ClientException
   {
      public ForbiddenException()
      {
      }

      public ForbiddenException(string message)
         : base(message)
      {
      }

      public ForbiddenException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      public override int StatusCode => 403;
   }
}
