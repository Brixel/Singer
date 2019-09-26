using System;

namespace Singer.DTOs
{
   public class EventSlotDTO
   {
      public Guid Id { get; set; }
      public DateTime StartDateTime { get; set; }
      public DateTime EndDateTime { get; set; }
   }
}
