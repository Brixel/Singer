using System;

namespace Singer.Controllers
{
   public class UpdatePasswordDTO
   {
      public Guid UserId { get; set; }
      public string Token { get; set; }

      public string NewPassword { get; set; }
   }
}
