using System;

namespace Singer.Helpers.Exceptions
{
   public class UserNotFoundException : NotFoundException
   {
      public UserNotFoundException()
      {
      }

      public UserNotFoundException(string message)
         : base(message)
      {
      }

      public UserNotFoundException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      public override string ClientMessage => "There is no user with the given id.";
   }
}
