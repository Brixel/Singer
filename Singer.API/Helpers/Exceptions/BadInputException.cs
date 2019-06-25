using System;

namespace Singer.Helpers.Exceptions
{
   public class BadInputException : ClientException
   {
      public BadInputException()
      {
      }

      public BadInputException(string message)
         : base(message)
      {
      }

      public BadInputException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      public override int StatusCode => 400;
      public override string ClientMessage => "The arguments you passed just destroyed Lichtenstein...";
   }
}
