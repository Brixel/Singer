using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Singer.Models;
using Singer.Resources;

namespace Singer.DTOs;

public class EventDescriptionDTO
{
    [Required(
      ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
      ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Id))]
    public Guid Id { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [StringLength(
       maximumLength: ValidationValues.MaxEventTitleLength,
       MinimumLength = ValidationValues.MinEventTitleLength,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthMustBeBetween),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Title))]
    public string Title { get; set; }

    [StringLength(
       maximumLength: ValidationValues.MaxDescriptionLength,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthCanMaximumBe),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.Description))]
    public string Description { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.StartDateTime))]
    public DateTime StartDate { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.EndDateTime))]
    public DateTime EndDate { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.AllowedAgeGroups))]
    public IReadOnlyList<AgeGroup> AgeGroups { get; set; }

    [Required(
      ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
      ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Cost))]
    public decimal Cost { get; set; }

    [Required(
        ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
        ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
        ResourceType = typeof(DisplayNames),
        Name = nameof(DisplayNames.StartDateTime))]
    public DateTime StartDateTime { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.EndDateTime))]
    public DateTime EndDateTime { get; set; }
}
