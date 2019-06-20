using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Singer.Models;

namespace Singer.DTOs
{
   public class CreateLegalGuardianUserDTO : CreateUserDTO
   {
      [DisplayName("Zorggebruikers")]
      public List<Guid> CareUsers { get; set; }
   }
}
