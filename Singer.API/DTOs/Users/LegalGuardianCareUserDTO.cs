using System;
using System.ComponentModel.DataAnnotations;

using Singer.Resources;

namespace Singer.DTOs.Users;

public class LegalGuardianCareUserDTO
{
    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.LegalGuardianId))]
    public Guid LegalGuardianId { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.CareUserId))]
    public Guid CareUserId { get; set; }
}
