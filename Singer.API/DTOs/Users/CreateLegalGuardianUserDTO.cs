using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Singer.DTOs.Users
{
   public class CreateLegalGuardianUserDTO : CreateUserDTO
   {
      [DisplayName("Zorggebruikers")]
      public List<Guid> CareUsers { get; set; }
      public string Address { get; set; }
      public string City { get; set; }
      public string PostalCode { get; set; }
      public string Country { get; set; }
   }
}
