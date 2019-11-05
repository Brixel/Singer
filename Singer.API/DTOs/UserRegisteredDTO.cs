using System;
using Singer.Models;

namespace Singer.DTOs
{
   public class UserRegisteredDTO
   {
      public Guid CareUserId { get; set; }
      public bool IsRegistered { get; set; }
      public int PendingStatussesRemaining { get; set; }
      public RegistrationStatus Status { get; set; }
   }
}
