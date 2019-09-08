using System;

namespace Singer.DTOs.Users
{
   public class LegalGuardianCareUserDTO
   {
      public Guid LegalGuardianId { get; set; }
      public Guid CareUserId { get; set; }
   }
}
