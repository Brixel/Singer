using Singer.Models;
using Singer.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs.Users
{
   public class CareUserDTO : UserDTO
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
      [StringLength(
         maximumLength: ValidationValues.CaseNumberLength,
         MinimumLength = ValidationValues.CaseNumberLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldMustHaveChars),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.CaseNumber))]
      public string CaseNumber { get; set; }

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
      public EventLocation NormalDaycareLocation { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.VacationDaycareLocation))]
      public EventLocation VacationDaycareLocation { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.HasResources))]
      public bool HasResources { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.LegalGuardianUsers))]
      public List<LinkedLegalGuardianDTO> LegalGuardianUsers { get; set; }
   }

   public class CreateCareUserDTO : CreateUserDTO
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
      [StringLength(
         maximumLength: ValidationValues.CaseNumberLength,
         MinimumLength = ValidationValues.CaseNumberLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldMustHaveChars),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.CaseNumber))]
      public string CaseNumber { get; set; }

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
         Name = nameof(DisplayNames.HasNormalDayCare))]
      public bool HasNormalDayCare { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.HasVacationDayCare))]
      public bool HasVacationDayCare { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.NormalDaycareLocation))]
      public Guid? NormalDaycareLocationId { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.VacationDaycareLocation))]
      public Guid? VacationDaycareLocationId { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.HasResources))]
      public bool HasResources { get; set; }
   }

   public class UpdateCareUserDTO : UpdateUserDTO
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
      [StringLength(
         maximumLength: ValidationValues.CaseNumberLength,
         MinimumLength = ValidationValues.CaseNumberLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldMustHaveChars),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.CaseNumber))]
      public string CaseNumber { get; set; }

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
         Name = nameof(DisplayNames.HasNormalDayCare))]
      public bool HasNormalDayCare { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.HasVacationDayCare))]
      public bool HasVacationDayCare { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.NormalDaycareLocation))]
      public Guid? NormalDaycareLocationId { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.VacationDaycareLocation))]
      public Guid? VacationDaycareLocationId { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.HasResources))]
      public bool HasResources { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.LegalGuardianUsersToAdd))]
      public List<Guid> LegalGuardianUsersToAdd { get; set; }

      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.LegalGuardianUsersToRemove))]
      public List<Guid> LegalGuardianUsersToRemove { get; set; }
   }
}
