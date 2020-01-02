using System;

namespace Singer.DTOs.Users
{
   public class UpdatePasswordDTO
   {
      public Guid UserId { get; set; }
      public string Token { get; set; }

      public string NewPassword { get; set; }
   }
}
