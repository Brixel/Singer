using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs.Users
{
   public class LinkedLegalGuardianDTO : UserDTO
   {
      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "{0} mag maximaal {1} karakters bevatten.")]
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
}
