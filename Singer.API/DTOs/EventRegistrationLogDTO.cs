using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Singer.DTOs
{
   public class EventRegistrationLogDTO
   {
      public Guid Id { get; set; }
      public Guid EventRegistrationId { get; set; }
      public string EventTitle { get; set; }
      public string CareUser { get; set; }
      public List<string> LegalGuardians { get; set; }
      public DateTime CreationDateTimeUTC { get; set; }
      public DateTime EventSlotStartDateTime { get; set; }
      public DateTime EventSlotEndDateTime { get; set; }
   }
}
