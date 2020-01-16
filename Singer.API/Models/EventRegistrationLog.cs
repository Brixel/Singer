using System;
using System.ComponentModel.DataAnnotations.Schema;
using Singer.Helpers;
using Singer.Models.Users;

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

      [ForeignKey(nameof(User))]
      public Guid ExecutedByUserId { get; set; }
      public User User { get; set; }

      public bool ActionTaken()
      {
         return EmailSent;
      }

      protected EventRegistrationLog(Guid eventRegistrationId,
         Guid executedByUserId, EventRegistrationChanges registrationChange)
      {
         EventRegistrationId = eventRegistrationId;
         EventRegistrationChanges = registrationChange;
         EmailSent = false;
         ExecutedByUserId = executedByUserId;
         CreationDateTimeUTC = DateTime.UtcNow;
      }

      protected EventRegistrationLog()
      {
      }
   }

   public class EventRegistrationStatusChange : EventRegistrationLog
   {

      public RegistrationStatus NewStatus { get; set; }

      public RegistrationStatus PreviousStatus { get; set; }

      public EventRegistrationStatusChange(Guid eventRegistrationId,
         Guid executedByUserId, RegistrationStatus previousStatus,
         RegistrationStatus newStatus) :
         base(eventRegistrationId, executedByUserId, EventRegistrationChanges.RegistrationStatusChange)
      {
         PreviousStatus = previousStatus;
         NewStatus = newStatus;
      }

      public static EventRegistrationStatusChange Create(Guid eventRegistrationId,
         Guid executedByUserId,
         RegistrationStatus previousStatus,
         RegistrationStatus newStatus)
      {
         return new EventRegistrationStatusChange(eventRegistrationId, executedByUserId, previousStatus, newStatus);
      }
   }

   public class EventRegistrationLocationChange : EventRegistrationLog
   {
      public Guid NewLocationId { get; set; }

      public Guid PreviousLocationId { get; set; }

      public EventRegistrationLocationChange()
      {
         
      }
      public EventRegistrationLocationChange(Guid eventRegistrationId, Guid executedByUserId, Guid previousLocationId, Guid newLocationId) :
         base(eventRegistrationId, executedByUserId, EventRegistrationChanges.LocationChange)
      {
         PreviousLocationId = previousLocationId;
         NewLocationId = newLocationId;

      }

      public static EventRegistrationLocationChange Create(Guid eventRegistrationId, Guid executedByUserId,
         Guid previousLocationId, Guid newLocationId)
      {
         return new EventRegistrationLocationChange(eventRegistrationId, executedByUserId, previousLocationId, newLocationId);
      }
   }

}
