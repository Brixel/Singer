using System;
using Singer.Resources;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs.Users
{
   public class AdminUserDTO : UserDTO
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
      public string UserName { get; set; }

      public Guid UserId { get; set; }
   }

   public class CreateAdminUserDTO : CreateUserDTO
   {
   }

   public class UpdateAdminUserDTO : UpdateUserDTO
   {
   }
}
