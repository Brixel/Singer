using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Singer.Helpers;

namespace Singer.Models
{
   public class EventRegistrationLog : IIdentifiable
   {
      public Guid Id { get; set; }

      [ForeignKey(nameof(EventRegistration))]
      public Guid EventRegistrationId { get; set; }
      public EventRegistration EventRegistration { get; set; }
      public EventRegistrationChanges EventRegistrationChanges { get; set; }
      public bool EmailSent { get; set; }
      public DateTime CreationDateTimeUTC { get; set; }

      public bool ActionTaken()
      {
         return EmailSent;
      }

      private EventRegistrationLog(Guid eventRegistrationId, EventRegistrationChanges registrationChange)
      {
         EventRegistrationId = eventRegistrationId;
         EventRegistrationChanges = registrationChange;
         EmailSent = false;
         CreationDateTimeUTC = DateTime.UtcNow;
      }

      public static EventRegistrationLog Create(Guid eventRegistrationId,
         EventRegistrationChanges registrationChange)
      {
         return new EventRegistrationLog(eventRegistrationId, registrationChange);
      }
   }
}
