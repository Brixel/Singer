using Microsoft.AspNetCore.Identity;

namespace Singer.Data.Models
{
   public class User : IdentityUser
   {
      public string Name { get; set; }
   }
}
