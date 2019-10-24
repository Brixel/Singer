using System;
using System.Collections.Generic;
using Singer.Models;

namespace Singer.DTOs
{
   public class EventDescriptionDTO
   {
      public string Title { get; set; }
      public string Description { get; set; }
      public DateTime StartDate { get; set; }
      public DateTime EndDate { get; set; }
      public IReadOnlyList<AgeGroup> AgeGroups { get; set; }
   }
}
