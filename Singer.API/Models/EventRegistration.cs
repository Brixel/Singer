using System;
using Singer.Helpers;
using Singer.Models.Users;

namespace Singer.Models
{
   public class EventRegistration : IIdentifiable
   {
      public Guid Id { get; set; }
      public Guid EventId { get; set; }

      public Event Event { get; set; }
      public Guid CareUserId { get; set; }

      public CareUser CareUser { get; set; }

      public RegistrationStatus Status { get; set; }
   }
}
