using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Singer.Helpers;
using Singer.Models;
using Singer.Resources;

namespace Singer.DTOs;

public class EventDTO : IIdentifiable
{
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.Id))]
    public Guid Id { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [StringLength(
       maximumLength: ValidationValues.MaxEventTitleLength,
       MinimumLength = ValidationValues.MinEventTitleLength,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthMustBeBetween),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.Title))]
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
       Name = nameof(DisplayNames.AllowedAgeGroups))]
    public List<AgeGroup> AllowedAgeGroups { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.Location))]
    public SingerLocationDTO Location { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Range(
       minimum: ValidationValues.MinMaxRegistrants,
       maximum: ValidationValues.MaxMaxRegistrants,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldMustBeBetween),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.MaxRegistrants))]
    public int MaxRegistrants { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Range(
       minimum: ValidationValues.MinEventCost,
       maximum: ValidationValues.MaxEventCost,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldMustBeBetween),
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

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.StartRegistrationDateTime))]
    public DateTime StartRegistrationDateTime { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.EndRegistrationDateTime))]
    public DateTime EndRegistrationDateTime { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.FinalCancellationDateTime))]
    public DateTime FinalCancellationDateTime { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.RegistrationOnDailyBasis))]
    public bool RegistrationOnDailyBasis { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.HasDayCareBefore))]
    public bool HasDayCareBefore { get; set; }

    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.DayCareBeforeStartDateTime))]
    public DateTime? DayCareBeforeStartDateTime { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.HasDayCareAfter))]
    public bool HasDayCareAfter { get; set; }

    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.DayCareAfterEndDateTime))]
    public DateTime? DayCareAfterEndDateTime { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.EventSlots))]
    public IList<EventSlotDTO> EventSlots { get; set; }
}

public class CreateEventDTO
{
    [Required(
      ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
      ErrorMessageResourceType = typeof(ErrorMessages))]
    [StringLength(
      maximumLength: ValidationValues.MaxEventTitleLength,
      MinimumLength = ValidationValues.MinEventTitleLength,
      ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthMustBeBetween),
      ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
      ResourceType = typeof(DisplayNames),
      Name = nameof(DisplayNames.Title))]
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
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired), ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.AllowedAgeGroups))]
    public List<AgeGroup> AllowedAgeGroups { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired), ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.LocationId))]
    public Guid LocationId { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired), ErrorMessageResourceType = typeof(ErrorMessages))]
    [Range(
       minimum: ValidationValues.MinMaxRegistrants,
       maximum: ValidationValues.MaxMaxRegistrants,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldMustBeBetween),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.MaxRegistrants))]
    public int MaxRegistrants { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Range(
       minimum: ValidationValues.MinEventCost,
       maximum: ValidationValues.MaxEventCost,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldMustBeBetween),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.Cost))]
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

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.StartRegistrationDateTime))]
    public DateTime StartRegistrationDateTime { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.EndRegistrationDateTime))]
    public DateTime EndRegistrationDateTime { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.FinalCancellationDateTime))]
    public DateTime FinalCancellationDateTime { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.RegistrationOnDailyBasis))]
    public bool RegistrationOnDailyBasis { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.HasDayCareBefore))]
    public bool HasDayCareBefore { get; set; }

    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.DayCareBeforeStartDateTime))]
    public DateTime? DayCareBeforeStartDateTime { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.HasDayCareAfter))]
    public bool HasDayCareAfter { get; set; }

    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.DayCareAfterEndDateTime))]
    public DateTime? DayCareAfterEndDateTime { get; set; }

    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.RepeatSettings))]
    public RepeatSettingsDTO RepeatSettings { get; set; }
}

public class UpdateEventDTO
{
    [Required(
      ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
      ErrorMessageResourceType = typeof(ErrorMessages))]
    [StringLength(
      maximumLength: ValidationValues.MaxEventTitleLength,
      MinimumLength = ValidationValues.MinEventTitleLength,
      ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthMustBeBetween),
      ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
      ResourceType = typeof(DisplayNames),
      Name = nameof(DisplayNames.Title))]
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
       Name = nameof(DisplayNames.AllowedAgeGroups))]
    public List<AgeGroup> AllowedAgeGroups { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.LocationId))]
    public string LocationId { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Range(
       minimum: ValidationValues.MinMaxRegistrants,
       maximum: ValidationValues.MaxMaxRegistrants,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldMustBeBetween),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.MaxRegistrants))]
    public int MaxRegistrants { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Range(
       minimum: ValidationValues.MinEventCost,
       maximum: ValidationValues.MaxEventCost,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldMustBeBetween),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.Cost))]
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

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.StartRegistrationDateTime))]
    public DateTime StartRegistrationDateTime { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.EndRegistrationDateTime))]
    public DateTime EndRegistrationDateTime { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.FinalCancellationDateTime))]
    public DateTime FinalCancellationDateTime { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.RegistrationOnDailyBasis))]
    public bool RegistrationOnDailyBasis { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.HasDayCareBefore))]
    public bool HasDayCareBefore { get; set; }

    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.DayCareBeforeStartDateTime))]
    public DateTime? DayCareBeforeStartDateTime { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.HasDayCareAfter))]
    public bool HasDayCareAfter { get; set; }

    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.DayCareAfterEndDateTime))]
    public DateTime? DayCareAfterEndDateTime { get; set; }
}
