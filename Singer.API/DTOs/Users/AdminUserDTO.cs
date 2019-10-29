using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs.Users
{
   public class AdminUserDTO : UserDTO
   {
      [Required]
      [StringLength(maximumLength: 50,
         ErrorMessage = "Het {0} moet een lengte hebben van {2} karakters.",
         MinimumLength = 2)]
      [DisplayName("Gebruikersnaam")]
      public string UserName { get; set; }
   }

   public class CreateAdminUserDTO : CreateUserDTO
   {
   }

   public class UpdateAdminUserDTO : UpdateUserDTO
   {
   }
}
