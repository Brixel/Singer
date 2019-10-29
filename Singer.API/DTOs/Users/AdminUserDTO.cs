using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs.Users
{
   public class AdminUserDTO : UserDTO
   {
      [Required]
      [StringLength(maximumLength: 50,
         ErrorMessage = "{0} moet een minimale lengte van {2} en maximale lengte van {1} karakters hebben.",
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
