using System;
using System.Collections.Generic;
using Singer.Models;

namespace Singer.DTOs
{
   public class CreateEventDTO
   {
      public string Title { get; set; }
      public string Description { get; set; }
      public List<AgeGroup> AllowedAgeGroups { get; set; }
      public string LocationId { get; set; }
      public int MaxRegistrants { get; set; }
      public decimal Cost { get; set; }
      public DateTimeOffset StartDate { get; set; }
      public DateTimeOffset DailyStartTime { get; set; }
      public DateTimeOffset DailyEndTime { get; set; }
      public DateTimeOffset LastCancellationDate { get; set; }
      public bool FullTimeSpanRegRequired { get; set; }
      public bool BeforeAndAfterCare { get; set; }
   }
}
