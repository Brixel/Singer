using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs.Users
{
   public class LegalGuardianUserDTO : UserDTO
   {
      public List<LinkedCareUserDTO> CareUsers { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "{0} moet een minimale lengte van {2} en maximale lengte van {1} karakters hebben.")]
      [DisplayName("Adres")]
      public string Address { get; set; }

      [Required]
      [StringLength(maximumLength: 10, ErrorMessage = "{0} mag maximaal {1} karakters bevatten.")]
      [DisplayName("Postcode")]
      public string PostalCode { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "{0} mag maximaal {1} karakters bevatten.")]
      [DisplayName("Gemeente")]
      public string City { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "{0} mag maximaal {1} karakters bevatten.")]
      [DisplayName("Land")]
      public string Country { get; set; }
   }

   public class CreateLegalGuardianUserDTO : CreateUserDTO
   {
      [DisplayName("Zorggebruikers")]
      public List<Guid> CareUsers { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "{0} mag maximaal {1} karakters bevatten.")]
      [DisplayName("Adres")]
      public string Address { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "{0} mag maximaal {1} karakters bevatten.")]
      [DisplayName("Gemeente")]
      public string City { get; set; }

      [Required]
      [StringLength(maximumLength: 10, ErrorMessage = "{0} mag maximaal {1} karakters bevatten.")]
      [DisplayName("Postcode")]
      public string PostalCode { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "{0} mag maximaal {1} karakters bevatten.")]
      [DisplayName("Land")]
      public string Country { get; set; }
   }

   public class UpdateLegalGuardianUserDTO : UpdateUserDTO
   {
      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "{0} mag maximaal {1} karakters bevatten.")]
      [DisplayName("Adres")]
      public string Address { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "{0} mag maximaal {1} karakters bevatten.")]
      [DisplayName("Gemeente")]
      public string City { get; set; }

      [Required]
      [StringLength(maximumLength: 10, ErrorMessage = "{0} mag maximaal {1} karakters bevatten.")]
      [DisplayName("Postcode")]
      public string PostalCode { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "{0} mag maximaal {1} karakters bevatten.")]
      [DisplayName("Land")]
      public string Country { get; set; }

      [DisplayName("Toe te voegen zorggebruikers")]
      public List<Guid> CareUsersToAdd { get; set; }

      [DisplayName("Te verwijderen zorggebruikers")]
      public List<Guid> CareUsersToRemove { get; set; }
   }
}
