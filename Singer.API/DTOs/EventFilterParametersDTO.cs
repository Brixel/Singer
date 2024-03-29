using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Singer.Models;
using Singer.Resources;

namespace Singer.DTOs;

public class EventFilterParametersDTO
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
       Name = nameof(DisplayNames.Text))]
    public string Text { get; set; }
}
