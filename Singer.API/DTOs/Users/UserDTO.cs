using Singer.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs.Users
{
   public class UserDTO : IUserDTO
   {
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.Id))]
      public Guid Id { get; set; }

      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [StringLength(
         maximumLength: ValidationValues.MaxNameLength,
         MinimumLength = ValidationValues.MinNameLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthMustBeBetween),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.FirstName))]
      public string FirstName { get; set; }

      [Required(
        ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
        ErrorMessageResourceType = typeof(ErrorMessages))]
      [StringLength(
         maximumLength: ValidationValues.MaxNameLength,
         MinimumLength = ValidationValues.MinNameLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthMustBeBetween),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.LastName))]
      public string LastName { get; set; }

      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [StringLength(
         maximumLength: ValidationValues.MaxEmailLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthCanMaximumBe),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [EmailAddress(
         ErrorMessageResourceName = nameof(ErrorMessages.InvalidEmail),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.Email))]
      public string Email { get; set; }
   }

   public class CreateUserDTO : ICreateUserDTO
   {
      [Required(
        ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
        ErrorMessageResourceType = typeof(ErrorMessages))]
      [StringLength(
         maximumLength: ValidationValues.MaxNameLength,
         MinimumLength = ValidationValues.MinNameLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthMustBeBetween),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.UserName))]
      public string FirstName { get; set; }

      [Required(
        ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
        ErrorMessageResourceType = typeof(ErrorMessages))]
      [StringLength(
         maximumLength: ValidationValues.MaxNameLength,
         MinimumLength = ValidationValues.MinNameLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthMustBeBetween),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.UserName))]
      public string LastName { get; set; }

      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [StringLength(
         maximumLength: ValidationValues.MaxEmailLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthCanMaximumBe),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [EmailAddress(
         ErrorMessageResourceName = nameof(ErrorMessages.InvalidEmail),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.UserName))]
      public string Email { get; set; }
   }

   public class UpdateUserDTO : IUpdateUserDTO
   {
      [Required(
        ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
        ErrorMessageResourceType = typeof(ErrorMessages))]
      [StringLength(
         maximumLength: ValidationValues.MaxNameLength,
         MinimumLength = ValidationValues.MinNameLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthMustBeBetween),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.UserName))]
      public string FirstName { get; set; }

      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [StringLength(
         maximumLength: ValidationValues.MaxNameLength,
         MinimumLength = ValidationValues.MinNameLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthMustBeBetween),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.UserName))]
      public string LastName { get; set; }

      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [StringLength(
         maximumLength: ValidationValues.MaxEmailLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthCanMaximumBe),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [EmailAddress(
         ErrorMessageResourceName = nameof(ErrorMessages.InvalidEmail),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.UserName))]
      public string Email { get; set; }
   }
}
