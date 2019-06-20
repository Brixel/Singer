using System;
using System.ComponentModel;

namespace Singer.DTOs
{
   public class LegalGuardianUserDTO : CreateLegalGuardianUserDTO, IUserDTO
   {
      [DisplayName("Id")]
      public Guid Id { get; set; }

      [DisplayName("UserId")]
      public Guid UserId { get; set; }
   }
}
