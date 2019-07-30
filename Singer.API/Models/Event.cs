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
      public DateTime StartDate { get; set; }
      public DateTime DailyStartTime { get; set; }
      public DateTime DailyEndTime { get; set; }
      public DateTime LastCancellationDate { get; set; }
      public bool FullTimeSpanRegRequired { get; set; }
      public bool BeforeAndAfterCare { get; set; }
   }
}
