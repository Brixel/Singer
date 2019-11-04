using System;
using Singer.Models;

namespace Singer.DTOs
{
   public class EventCareUserRegistrationDTO
   {
      public Guid CareUserId { get; set; }
      public RegistrationStatus Status { get; set; }
   }
}
