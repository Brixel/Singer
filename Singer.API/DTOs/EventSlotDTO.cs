using System;
using System.ComponentModel.DataAnnotations;

using Singer.Helpers;
using Singer.Resources;

namespace Singer.DTOs;

public class EventSlotDTO : IIdentifiable
{
    [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.Id))]
    public Guid Id { get; set; }

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
    [Range(
       minimum: ValidationValues.MinCurrentRegistrants,
       maximum: ValidationValues.MaxCurrentRegistrants,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldMustBeBetween),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.CurrentRegistrants))]
    public int CurrentRegistrants { get; set; }


    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.EndDateTime))]
    public DateTime EndDateTime { get; set; }
}
