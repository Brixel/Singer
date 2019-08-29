using System.Collections.Generic;

namespace Singer.Configuration
{
   public static class Roles
   {
      public const string ROLE_ADMINISTRATOR = "Administrator";
      private const string ROLE_SOCIALSERVICES = "SocialServices";
      private const string ROLE_CARETAKER = "Caretaker";
      public const string ROLE_CAREUSER = "CareUser";

      public static List<string> _careUsers = new List<string>() { "user1", "user2", "user3" };

      public static List<string> ROLES = new List<string>()
      {
         ROLE_ADMINISTRATOR,
         ROLE_SOCIALSERVICES,
         ROLE_CARETAKER,
         ROLE_CAREUSER
      };
   }
}
