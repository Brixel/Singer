using System;
using Singer.Helpers;
using Singer.Models.Users;

namespace Singer.Models
{
   public class EventRegistration : IIdentifiable
   {
      public EventRegistration()
      {
         // Default the registrations are set the pending
         Status = RegistrationStatus.Pending;
      }
      public Guid Id { get; set; }
      public Guid EventSlotId { get; set; }

      public EventSlot EventSlot { get; set; }
      public Guid CareUserId { get; set; }

      public CareUser CareUser { get; set; }

      public RegistrationStatus Status { get; set; }
   }
}
