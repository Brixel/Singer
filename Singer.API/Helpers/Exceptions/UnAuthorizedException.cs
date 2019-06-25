using System;

namespace Singer.Helpers.Exceptions
{
   public class UnAuthorizedException : ClientException
   {
      public UnAuthorizedException()
      {
      }

      public UnAuthorizedException(string message)
         : base(message)
      {
      }

      public UnAuthorizedException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      public override int StatusCode => 401;
   }
}
