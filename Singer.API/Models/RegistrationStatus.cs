using System;

namespace Singer.Models
{
   [Flags]
   public enum RegistrationStatus
   {
      Pending = 0b001,
      Accepted = 0b010,
      Rejected = 0b100
   }
}
