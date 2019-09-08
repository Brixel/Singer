using Singer.DTOs.Users;
using Singer.Models;
using System;

namespace Singer.DTOs
{
   public class CreateEventRegistrationDTO
   {
      public Guid EventId { get; set; }
      public Guid CareUserId { get; set; }
      public RegistrationStatus Status { get; set; }
   }
}
