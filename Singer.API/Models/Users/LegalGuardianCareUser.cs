using System;

namespace Singer.Models.Users
{
   public class LegalGuardianCareUser
   {
      public Guid LegalGuardianId { get; set; }
      public LegalGuardianUser LegalGuardian { get; set; }
      public Guid CareUserId { get; set; }
      public CareUser CareUser { get; set; }
   }
}
