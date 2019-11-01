using Singer.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs
{
   public class SearchEventParamsDTO
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
   }
}
