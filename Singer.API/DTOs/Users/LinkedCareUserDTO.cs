using System;
using System.ComponentModel.DataAnnotations;

using Singer.Models;
using Singer.Resources;

namespace Singer.DTOs.Users;

public class LinkedCareUserDTO : UserDTO
{
    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.BirthDay))]
    [DataType(DataType.Date)]
    public DateTime BirthDay { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.AgeGroup))]
    public AgeGroup AgeGroup { get; set; }

    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.IsExtern))]
    public bool IsExtern { get; set; }

    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.HasTrajectory))]
    public bool HasTrajectory { get; set; }

    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.NormalDaycareLocation))]
    public SingerLocation NormalDaycareLocation { get; set; }

    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.VacationDaycareLocation))]
    public SingerLocation VacationDaycareLocation { get; set; }

}
