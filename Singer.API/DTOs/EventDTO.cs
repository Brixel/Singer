using System;
using System.Collections.Generic;
using Singer.Helpers;
using Singer.Models;

namespace Singer.DTOs
{
   public class EventDTO : IIdentifiable
   {
      public Guid Id { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public List<AgeGroup> AllowedAgeGroups { get; set; }
      public EventLocationDTO Location { get; set; }
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
