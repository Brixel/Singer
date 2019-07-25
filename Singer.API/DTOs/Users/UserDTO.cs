using System;

namespace Singer.DTOs.Users
{
   public class UserDTO : IUserDTO
   {
      public Guid Id { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Email { get; set; }
   }
}
