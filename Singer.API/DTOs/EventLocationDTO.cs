using Singer.Helpers;
using Singer.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs
{
   public class SingerLocationDTO : IIdentifiable
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
      [StringLength(
         maximumLength: ValidationValues.MaxNameLength,
         MinimumLength = ValidationValues.MinNameLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthMustBeBetween),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.Name))]
      public string Name { get; set; }

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

   public class CreateSingerLocationDTO
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
      [StringLength(
         maximumLength: ValidationValues.MaxNameLength,
         MinimumLength = ValidationValues.MinNameLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthMustBeBetween),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.Name))]
      public string Name { get; set; }

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

   public class UpdateSingerLocationDTO
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
      [StringLength(
         maximumLength: ValidationValues.MaxNameLength,
         MinimumLength = ValidationValues.MinNameLength,
         ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthMustBeBetween),
         ErrorMessageResourceType = typeof(ErrorMessages))]
      [Display(
         ResourceType = typeof(DisplayNames),
         Name = nameof(DisplayNames.Name))]
      public string Name { get; set; }

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

   public class SingerLocationSearchDTO : SearchDTOBase
   {

   }
}
