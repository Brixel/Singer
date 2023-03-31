using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Singer.Resources;

namespace Singer.DTOs.Users;

public class LegalGuardianUserDTO : UserDTO
{
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.CareUsers))]
    public List<LinkedCareUserDTO> CareUsers { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [StringLength(
       maximumLength: ValidationValues.MaxAddressLength,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthCanMaximumBe),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Address))]
    public string Address { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [StringLength(
       maximumLength: ValidationValues.MaxPostalCodeLength,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthCanMaximumBe),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.PostalCode))]
    public string PostalCode { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [StringLength(
       maximumLength: ValidationValues.MaxCityLength,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthCanMaximumBe),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.City))]
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
    public Guid UserId { get; set; }
}

public class CreateLegalGuardianUserDTO : CreateUserDTO
{
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.CareUsers))]
    public List<Guid> CareUsers { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [StringLength(
       maximumLength: ValidationValues.MaxAddressLength,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthCanMaximumBe),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Address))]
    public string Address { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [StringLength(
       maximumLength: ValidationValues.MaxPostalCodeLength,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthCanMaximumBe),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.PostalCode))]
    public string PostalCode { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [StringLength(
       maximumLength: ValidationValues.MaxCityLength,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthCanMaximumBe),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.City))]
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

public class UpdateLegalGuardianUserDTO : UpdateUserDTO
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
    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.PostalCode))]
    public string PostalCode { get; set; }

    [Required(
       ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [StringLength(
       maximumLength: ValidationValues.MaxCityLength,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthCanMaximumBe),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.City))]
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

    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.CareUsersToAdd))]
    public List<Guid> CareUsersToAdd { get; set; }

    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.CareUsersToRemove))]
    public List<Guid> CareUsersToRemove { get; set; }
}
