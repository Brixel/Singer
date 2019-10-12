using System;
using System.Collections.Generic;
using Singer.Models;

namespace Singer.DTOs
{
   public class UpdateEventDTO
   {
      public string Title { get; set; }
      public string Description { get; set; }
      public List<AgeGroup> AllowedAgeGroups { get; set; }
      public string LocationId { get; set; }
      public int MaxRegistrants { get; set; }
      public decimal Cost { get; set; }
      public DateTime StartDateTime { get; set; }
      public DateTime EndDateTime { get; set; }
      public DateTime StartRegistrationDateTime { get; set; }
      public DateTime EndRegistrationDateTime { get; set; }
      public DateTime FinalCancellationDateTime { get; set; }
      public bool RegistrationOnDailyBasis { get; set; }
      public bool HasDayCareBefore { get; set; }
      public DateTime DayCareBeforeStartDateTime { get; set; }
      public bool HasDayCareAfter { get; set; }
      public DateTime DayCareAfterEndDateTime { get; set; }
   }
}