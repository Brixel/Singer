using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Singer.DTOs
{
   public class AboutDTO
   {
      public string ApiVersion { get; set; }

      public UserInfoDTO UserInfo { get; set; }
   }

   public class UserInfoDTO
   {
      public bool IsAdmin { get; set; }
   }
}
