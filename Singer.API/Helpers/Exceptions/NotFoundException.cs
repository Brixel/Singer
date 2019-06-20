using System;

namespace Singer.Helpers.Exceptions
{
   public class NotFoundException : Exception
   {
      public NotFoundException()
      {
      }

      public NotFoundException(string message) : base(message)
      {
      }
   }
}
