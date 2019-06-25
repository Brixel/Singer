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
      public override string ClientMessage => "One does not simply make a request to this API and gets all the data.";
   }
}
