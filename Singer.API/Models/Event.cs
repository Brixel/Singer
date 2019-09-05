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

      public List<EventRegistration> Registrations { get; set; }
   }
}
