using System;
using System.Collections.Generic;

namespace Singer.DTOs
{
   public class EventSlotRegistrationsDTO
   {
      public Guid Id { get; set; }
      public DateTime StartDateTime { get; set; }
      public DateTime EndDateTime { get; set; }
      public IList<EventCareUserRegistrationDTO> Registrations { get; set; }
   }
}
