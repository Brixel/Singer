using System;
using System.ComponentModel.DataAnnotations.Schema;
using Singer.Helpers;

namespace Singer.Models
{
   public abstract class EventRegistrationLog : IIdentifiable
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

      protected EventRegistrationLog(Guid eventRegistrationId, EventRegistrationChanges registrationChange)
      {
         EventRegistrationId = eventRegistrationId;
         EventRegistrationChanges = registrationChange;
         EmailSent = false;
         CreationDateTimeUTC = DateTime.UtcNow;
      }
   }

   public class EventRegistrationStatusChange : EventRegistrationLog
   {

      public RegistrationStatus NewStatus { get; set; }

      public RegistrationStatus PreviousStatus { get; set; }
      public EventRegistrationStatusChange(Guid eventRegistrationId, RegistrationStatus previousStatus,
         RegistrationStatus newStatus) :
         base(eventRegistrationId, EventRegistrationChanges.RegistrationStatusChange)
      {
         PreviousStatus = previousStatus;
         NewStatus = newStatus;
      }

      public static EventRegistrationStatusChange Create(Guid eventRegistrationId,
         RegistrationStatus previousStatus,
         RegistrationStatus newStatus)
      {
         return new EventRegistrationStatusChange(eventRegistrationId, previousStatus, newStatus);
      }
   }

   public class EventRegistrationLocationChange : EventRegistrationLog
   {
      public Guid NewLocationIdId { get; set; }

      public Guid PreviousLocationId { get; set; }
      public EventRegistrationLocationChange(Guid eventRegistrationId, Guid previousLocationId, Guid newLocationId) :
         base(eventRegistrationId, EventRegistrationChanges.LocationChange)
      {
         PreviousLocationId = previousLocationId;
         NewLocationIdId = newLocationId;

      }

      public static EventRegistrationLocationChange Create(Guid eventRegistrationId,
         Guid previousLocationId, Guid newLocationId)
      {
         return new EventRegistrationLocationChange(eventRegistrationId, previousLocationId, newLocationId);
      }
   }

}
