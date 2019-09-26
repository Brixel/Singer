using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs.Users
{
   public class
      CreateUserDTO : ICreateUserDTO
   {
      [Required]
      [StringLength(maximumLength: 255,
         ErrorMessage = "De {0} moet een lengte hebben van minstens {2} en maximum {1} karakters.",
         MinimumLength = 3)]
      [DisplayName("Voornaam")]
      public string FirstName { get; set; }
      [Required]
      [StringLength(maximumLength: 255,
         ErrorMessage = "De {0} moet een lengte hebben van minstens {2} en maximum {1} karakters.",
         MinimumLength = 3)]
      [DisplayName("Achternaam")]
      public string LastName { get; set; }

      [Required]
      [EmailAddress]
      [DisplayName("E-mail adres")]
      public string Email { get; set; }
   }
}
