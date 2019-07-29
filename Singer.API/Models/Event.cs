using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Singer.Helpers;

namespace Singer.Models
{
   public class Event : IIdentifiable
   {
      public Guid Id { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public AgeGroup AllowedAgeGroups { get; set; }
      [ForeignKey(nameof(Location))]
      public Guid LocationId { get; set; }
      public EventLocation Location { get; set; }
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
