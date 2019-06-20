
using System;

namespace Singer.Helpers.Exceptions
{
   public class BadInputException : Exception
   {
      public BadInputException()
      {
      }

      public BadInputException(string message)
        : base(message)
      {
      }
   }
}
