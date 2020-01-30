using System;
using System.Collections.Generic;
using Singer.Models;

namespace Singer.DTOs
{
   public class EventRegistrationLogCareUserDTO
   {
      public Guid CareUserId { get; set; }
      public string CareUser { get; set; }
      public List<LegalGuardianDTO> LegalGuardians { get; set; }
      public DateTime CreationDateTimeUTC { get; set; }

      public IReadOnlyList<CareUserRegistrationStateChangedDTO> RegistrationStateChanges { get; set; }
      public IReadOnlyList<CareUserRegistrationLocationChangedDTO> RegistrationLocationChanges { get; set; }

      public EventRegistrationLogCareUserDTO()
      {
         RegistrationStateChanges = new List<CareUserRegistrationStateChangedDTO>();
         RegistrationLocationChanges = new List<CareUserRegistrationLocationChangedDTO>();
      }

      public class LegalGuardianDTO
      {
         public string Name { get; set; }
         public string Email { get; set; }
      }
   }

   public class CareUserRegistrationStateChangedDTO
   {
      public Guid EventRegistrationId { get; set; }
      public string EventTitle { get; set; }
      public DateTime EventSlotStartDateTime { get; set; }
      public DateTime EventSlotEndDateTime { get; set; }
      public RegistrationStatus NewStatus { get; set; }
   }

   public class CareUserRegistrationLocationChangedDTO
   {
      public Guid EventRegistrationId { get; set; }
      public string EventTitle { get; set; }
      public DateTime EventSlotStartDateTime { get; set; }
      public DateTime EventSlotEndDateTime { get; set; }
      public string NewLocation { get; set; }
   }
}
