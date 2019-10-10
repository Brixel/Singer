using Singer.Helpers;
using Singer.Models;
using System;

namespace Singer.DTOs
{
   public class EventSlotDTO : IIdentifiable
   {
      public Guid Id { get; set; }
      public DateTime StartDateTime { get; set; }
      public DateTime EndDateTime { get; set; }
   }

   public class EventSlotRegistrationDTO : IIdentifiable
   {
      public Guid Id { get; set; }
      public RegistrationStatus Status { get; set; }
      public Guid EventSlotId { get; set; }
   }
}
