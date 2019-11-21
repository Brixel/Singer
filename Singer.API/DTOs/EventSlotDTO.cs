using Singer.Helpers;
using System;

namespace Singer.DTOs
{
   public class EventSlotDTO : IIdentifiable
   {
      public Guid Id { get; set; }
      public int CurrentRegistrants { get; set; }
      public DateTime StartDateTime { get; set; }
      public DateTime EndDateTime { get; set; }
   }
}
