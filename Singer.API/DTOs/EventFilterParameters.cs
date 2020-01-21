using Singer.Models;
using Singer.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs
{
   public class EventFilterParameters
   {
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.StartDateTime))]
      public DateTime? StartDate { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.EndDateTime))]
      public DateTime? EndDate { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.LocationId))]
      public Guid? LocationId { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.AllowedAgeGroups))]
      public List<AgeGroup> AllowedAgeGroups { get; set; }

      [StringLength(maximumLength: ValidationValues.MaxEventTitleLength)]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.Title))]
      public string Title { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.MaxCost))]
      public int? MaxCost { get; set; }
   }
}
