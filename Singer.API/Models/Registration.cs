using System;
using System.ComponentModel.DataAnnotations.Schema;
using Singer.Helpers;
using Singer.Models.Users;

namespace Singer.Models
{
   public class EventRegistration : IIdentifiable
   {
      public Guid Id { get; set; }
      public RegistrationStatus Status { get; set; }
      public EventRegistrationTypes EventRegistrationType { get; set; }
      public Guid? EventSlotId { get; set; }
      public EventSlot EventSlot { get; set; }
      public Guid CareUserId { get; set; }
      public CareUser CareUser { get; set; }

      [ForeignKey(nameof(DaycareLocation))]
      public Guid? DaycareLocationId { get; set; }
      public EventLocation DaycareLocation { get; set; }

      public DateTime StartDateTime { get; set; }
      public DateTime EndDateTime { get; set; }
      private EventRegistration()
      {
         // Default the registrations are set the pending
         Status = RegistrationStatus.Pending;
      }

      public static EventRegistration Create(EventRegistrationTypes eventRegistrationTypes,
         DateTime startDateTime, DateTime endDateTime)
      {
         return new EventRegistration(){
            EventRegistrationType = eventRegistrationTypes,
            StartDateTime = startDateTime,
            EndDateTime = endDateTime
         };
      }

      public static EventRegistration Create(Guid careUserId, Guid eventSlotId,
         DateTime startDateTime, DateTime endDateTime, RegistrationStatus status)
      {
         var registration = new EventRegistration
         {
            CareUserId = careUserId,
            EventSlotId = eventSlotId,
            EventRegistrationType = EventRegistrationTypes.EventSlotDriven,
            StartDateTime = startDateTime,
            EndDateTime = endDateTime,
            Status = status
         };
         return registration;
      }
   }

   public enum EventRegistrationTypes
   {
      EventSlotDriven = 1,
      DayCare = 2,
      NightCare = 3
   }
}
