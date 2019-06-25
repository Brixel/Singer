using System;

namespace Singer.Helpers.Exceptions
{
   public class InernalServerException : ServerException
   {
      public InernalServerException()
      {
      }

      public InernalServerException(string message)
         : base(message)
      {
      }

      public InernalServerException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      public override int StatusCode => 500;
   }
}
