using System;
using Microsoft.AspNetCore.Identity;

namespace Singer.Models
{
   public class User : IdentityUser<Guid>
   {
      public string Name { get; set; }
   }
}
