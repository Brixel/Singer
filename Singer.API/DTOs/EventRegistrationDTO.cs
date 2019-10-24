using System;
using System.Collections.Generic;
using Singer.DTOs.Users;
using Singer.Helpers;
using Singer.Models;

namespace Singer.DTOs
{
   public class EventRegistrationDTO
   {
      public Guid Id { get; set; }
      public EventSlotDTO EventSlot { get; set; }
      public EventDescriptionDTO EventDescription { get; set; }
      public CareUserDTO CareUser { get; set; }
      public RegistrationStatus Status { get; set; }
   }

   public class CreateEventRegistrationDTO
   {
      public Guid EventId { get; set; }
      public Guid CareUserId { get; set; }
      public RegistrationStatus Status { get; set; }
   }

   public class UpdateEventRegistrationDTO
   {
      public Guid EventSlotId { get; set; }
      public Guid CareUserId { get; set; }
      public RegistrationStatus Status { get; set; }
   }
}
