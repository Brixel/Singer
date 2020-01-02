using System;
using Singer.Models;

namespace Singer.DTOs
{
   public class EventCareUserRegistrationDTO
   {
      public Guid RegistrationId { get; set; }
      public Guid CareUserId { get; set; }
      public DaycareLocationDTO DaycareLocation { get; set; }
      public RegistrationStatus Status { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }

   }
}
