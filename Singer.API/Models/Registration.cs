using System;
using System.ComponentModel.DataAnnotations.Schema;
using Singer.Helpers;
using Singer.Models.Users;

namespace Singer.Models
{
   public class Registration : IIdentifiable
   {
      public Guid Id { get; set; }
      public RegistrationStatus Status { get; set; }
      public RegistrationTypes EventRegistrationType { get; set; }
      public Guid? EventSlotId { get; set; }
      public EventSlot EventSlot { get; set; }
      public Guid CareUserId { get; set; }
      public CareUser CareUser { get; set; }

      [ForeignKey(nameof(DaycareLocation))]
      public Guid? DaycareLocationId { get; set; }
      public EventLocation DaycareLocation { get; set; }

      public DateTime StartDateTime { get; set; }
      public DateTime EndDateTime { get; set; }
      private Registration()
      {
         // Default the registrations are set the pending
         Status = RegistrationStatus.Pending;
      }

      public static Registration Create(RegistrationTypes eventRegistrationTypes,
         DateTime startDateTime, DateTime endDateTime)
      {
         return new Registration()
         {
            CareUserId = careUserId,
            EventRegistrationType = eventRegistrationTypes,
            StartDateTime = startDateTime,
            EndDateTime = endDateTime
         };
      }

      public static Registration Create(Guid careUserId, Guid eventSlotId,
         DateTime startDateTime, DateTime endDateTime, RegistrationStatus status)
      {
         var registration = new Registration
         {
            CareUserId = careUserId,
            EventSlotId = eventSlotId,
            EventRegistrationType = RegistrationTypes.EventSlotDriven,
            StartDateTime = startDateTime,
            EndDateTime = endDateTime,
            Status = status
         };
         return registration;
      }
   }

   public enum RegistrationTypes
   {
      EventSlotDriven = 1,
      DayCare = 2,
      NightCare = 4
   }
}
