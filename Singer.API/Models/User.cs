using System;
using Microsoft.AspNetCore.Identity;

namespace Singer.Models
{
   public class User : IdentityUser<Guid>
   {
      public string FirstName { get; set; }
      public string LastName { get; set; }
   }
}
