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
      public int currentRegistrants { get; set; }
      public decimal Cost { get; set; }
      public DateTime StartDate { get; set; }
      public DateTime EndDate { get; set; }
      public DateTime StartRegistrationDate { get; set; }
      public DateTime EndRegistrationDate { get; set; }
      public DateTime DailyStartTime { get; set; }
      public DateTime DailyEndTime { get; set; }
      public DateTime FinalCancellationDate { get; set; }
      public bool RegistrationOnDailyBasis { get; set; }
      public bool HasDayCareBefore { get; set; }
      public DateTime DayCareBeforeStartTime { get; set; }
      public DateTime DayCareBeforeEndTime { get; set; }
      public bool HasDayCareAfter { get; set; }
      public DateTime DayCareAfterStartTime { get; set; }
      public DateTime DayCareAfterEndTime { get; set; }
   }
}
