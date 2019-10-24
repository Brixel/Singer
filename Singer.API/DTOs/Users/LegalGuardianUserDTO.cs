using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Singer.DTOs.Users
{
   public class LegalGuardianUserDTO : UserDTO
   {
      public List<LinkedCareUserDTO> CareUsers { get; set; }
      public string Address { get; set; }
      public string PostalCode { get; set; }
      public string City { get; set; }
      public string Country { get; set; }
   }

   public class CreateLegalGuardianUserDTO : CreateUserDTO
   {
      [DisplayName("Zorggebruikers")]
      public List<Guid> CareUsers { get; set; }
      public string Address { get; set; }
      public string City { get; set; }
      public string PostalCode { get; set; }
      public string Country { get; set; }
   }

   public class UpdateLegalGuardianUserDTO : UpdateUserDTO
   {
      [DisplayName("Zorggebruikers")]
      public List<Guid> CareUsers { get; set; }
      public string Address { get; set; }
      public string City { get; set; }
      public string PostalCode { get; set; }
      public string Country { get; set; }
      public List<Guid> CareUsersToAdd { get; set; }
      public List<Guid> CareUsersToRemove { get; set; }
   }
}
