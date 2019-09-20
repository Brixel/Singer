using System;
using Microsoft.AspNetCore.Identity;
using Singer.Helpers;

namespace Singer.Models.Users
{
   public class User : IdentityUser<Guid>, IIdentifiable
   {
      public string FirstName { get; set; }
      public string LastName { get; set; }
   }
}
