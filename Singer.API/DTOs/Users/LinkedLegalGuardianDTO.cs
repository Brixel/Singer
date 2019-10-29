using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs.Users
{
   public class LinkedLegalGuardianDTO : UserDTO
   {
      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "Het {0} moet een lengte hebben van {2} karakters.")]
      [DisplayName("Adres")]
      public string Address { get; set; }

      [Required]
      [StringLength(maximumLength: 10, ErrorMessage = "Het {0} moet een lengte hebben van {2} karakters.")]
      [DisplayName("Postcode")]
      public string PostalCode { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "Het {0} moet een lengte hebben van {2} karakters.")]
      [DisplayName("Gemeente")]
      public string City { get; set; }

      [Required]
      [StringLength(maximumLength: 50, ErrorMessage = "Het {0} moet een lengte hebben van {2} karakters.")]
      [DisplayName("Land")]
      public string Country { get; set; }
   }
}
