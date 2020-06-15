using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Singer.Helpers;

namespace Singer.Models
{
   public class Event : IIdentifiable, IArchivable
   {
      public Event()
      {
         EventSlots = new List<EventSlot>();
      }
      public Guid Id { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public AgeGroup AllowedAgeGroups { get; set; }
      [ForeignKey(nameof(Location))]
      public Guid LocationId { get; set; }
      public virtual SingerLocation Location { get; set; }
      public int MaxRegistrants { get; set; }
      public decimal Cost { get; set; }
      public DateTime StartRegistrationDateTime { get; set; }
      public DateTime EndRegistrationDateTime { get; set; }
      public DateTime FinalCancellationDateTime { get; set; }
      public bool RegistrationOnDailyBasis { get; set; }
      public bool HasDayCareBefore { get; set; }
      public DateTime DayCareBeforeStartDateTime { get; set; }
      public bool HasDayCareAfter { get; set; }
      public DateTime DayCareAfterEndDateTime { get; set; }
      public virtual IList<EventSlot> EventSlots { get; set; }
      public bool IsArchived { get; set; }
   }
}
