using System;
using Microsoft.AspNetCore.Identity;
using Singer.Helpers;

namespace Singer.Models
{
   public class User : IdentityUser<Guid>, IIdentifiable
   {
      public string FirstName { get; set; }
      public string LastName { get; set; }
   }
}
