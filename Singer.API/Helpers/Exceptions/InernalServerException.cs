using System;

namespace Singer.Helpers.Exceptions
{
   public class InternalServerException : ServerException
   {
      public InternalServerException()
      {
      }

      public InternalServerException(string message)
         : base(message)
      {
      }

      public InternalServerException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      public override int StatusCode => 500;

      public override string ClientMessage =>
         "Sorry something went wrong. The monkey that should return this page escaped.\r\n" +
         "We are working to find him back";
   }
}
