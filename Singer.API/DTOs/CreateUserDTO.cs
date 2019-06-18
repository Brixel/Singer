using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs
{
   public class CreateUserDTO
   {

      [Required]
      [StringLength(maximumLength: 255,
         ErrorMessage = "De {0} moet een lengte hebben van minstens {2} en maximum {1} karakters.",
         MinimumLength = 3)]
      [DisplayName("FirstName")]
      public string FirstName { get; set; }
      [Required]
      [StringLength(maximumLength: 255,
         ErrorMessage = "De {0} moet een lengte hebben van minstens {2} en maximum {1} karakters.",
         MinimumLength = 3)]
      [DisplayName("LastName")]
      public string LastName { get; set; }

      [Required]
      [EmailAddress]
      [DisplayName("E-mail address")]
      public string Email { get; set; }

      [Required]
      [StringLength(maximumLength: 50,
         ErrorMessage = "De {0} moet een lengte hebben van minstens {2} en maximum {1} karakters.",
         MinimumLength = 3)]
      [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "De {0} mag alleen letters en cijfers bevatten.")]
      [DisplayName("Gebruikersnaam")]
      public string UserName { get; set; }
   }
}
