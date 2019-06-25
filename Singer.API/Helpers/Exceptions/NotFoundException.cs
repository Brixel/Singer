using System;

namespace Singer.Helpers.Exceptions
{
   public class NotFoundException : ClientException
   {
      public NotFoundException()
      {
      }

      public NotFoundException(string message)
         : base(message)
      {
      }

      public NotFoundException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      public override int StatusCode => 404;
   }
}
