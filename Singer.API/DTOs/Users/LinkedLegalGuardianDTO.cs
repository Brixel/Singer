using Singer.Resources;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs.Users
{
   public class LinkedLegalGuardianDTO : UserDTO
   {
      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [StringLength(
         maximumLength: ValidationValues.MaxAddressLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthCanMaximumBe),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.Address))]
      public string Address { get; set; }

      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [StringLength(
         maximumLength: ValidationValues.MaxPostalCodeLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthCanMaximumBe),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.PostalCode))]
      public string PostalCode { get; set; }

      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [StringLength(
         maximumLength: ValidationValues.MaxCityLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthCanMaximumBe),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.City))]
      public string City { get; set; }

      [Required(
         ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [StringLength(
         maximumLength: ValidationValues.MaxCountryLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthCanMaximumBe),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.Country))]
      public string Country { get; set; }
   }
}
