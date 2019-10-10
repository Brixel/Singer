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
   }

   public class CreateEventRegistrationDTO
   {
      public Guid EventId { get; set; }
      public Guid CareUserId { get; set; }
      public RegistrationStatus Status { get; set; }
   }

   public class UpdateEventRegistrationDTO
   {
      public Guid EventId { get; set; }
      public CareUserDTO CareUser { get; set; }
      public RegistrationStatus Status { get; set; }
   }
}
