using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Singer.Helpers;
using Singer.Models.Users;

namespace Singer.Models
{
   public class EventRegistration : IIdentifiable
   {
      public Guid Id { get; set; }
      public RegistrationStatus Status { get; set; }
      public Guid EventSlotId { get; set; }
      public EventSlot EventSlot { get; set; }
      public Guid CareUserId { get; set; }
      public CareUser CareUser { get; set; }

      [ForeignKey(nameof(DaycareLocation))]
      public Guid? DaycareLocationId { get; set; }
      public EventLocation DaycareLocation { get; set; }
      public EventRegistration()
      {
         // Default the registrations are set the pending
         Status = RegistrationStatus.Pending;
      }
   }
}
