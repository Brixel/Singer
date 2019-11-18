using System;
using System.Collections.Generic;
using Singer.Models;

namespace Singer.DTOs
{
   public class EventDescriptionDTO
   {
      public Guid Id { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public DateTime StartDateTime { get; set; }
      public DateTime EndDateTime { get; set; }
      public IReadOnlyList<AgeGroup> AgeGroups { get; set; }
   }
}
