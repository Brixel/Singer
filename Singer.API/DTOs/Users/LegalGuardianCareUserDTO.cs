using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs.Users
{
   public class LegalGuardianCareUserDTO
   {
      [Required]
      [DisplayName("Voogd id")]
      public Guid LegalGuardianId { get; set; }

      [Required]
      [DisplayName("Zorggebruiker id")]
      public Guid CareUserId { get; set; }
   }
}
