using System;
using System.Collections.Generic;
using Singer.Helpers;

namespace Singer.Models
{
   public class EventSlot : IIdentifiable
   {
      public Guid Id { get; set; }
      public Guid EventId { get; set; }
      public Event Event { get; set; }
      public DateTime StartDateTime { get; set; }
      public DateTime EndDateTime { get; set; }

      public IList<EventRegistration> Registrations { get; set; }
   }
}
