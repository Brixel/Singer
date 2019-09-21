using System;
using System.Collections.Generic;
using Singer.DTOs.Users;
using Singer.Helpers;
using Singer.Models;

namespace Singer.DTOs
{
   public class EventRegistrationDTO : IIdentifiable
   {
      public Guid Id { get; set; }
      public Guid EventId { get; set; }
      public CareUserDTO CareUser { get; set; }
      public IReadOnlyList<EventSlotRegistrationDTO> EventSlots { get; set; }

      public class EventSlotRegistrationDTO : IIdentifiable
      {
         public Guid Id { get; set; }
         public RegistrationStatus Status { get; set; }
         public Guid EventSlotId { get; set; }
      }
   }
}
