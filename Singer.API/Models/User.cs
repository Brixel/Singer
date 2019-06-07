using Microsoft.AspNetCore.Identity;

namespace Singer.Models
{
   public class User : IdentityUser
   {
      public string Name { get; set; }
   }
}
