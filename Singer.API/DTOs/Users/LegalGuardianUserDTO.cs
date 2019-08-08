using System;
using System.Collections.Generic;

namespace Singer.DTOs.Users
{
   public class LegalGuardianUserDTO : UserDTO
   {
      public List<CareUserDTO> CareUsers { get; set; }
      public string Address { get; set; }
      public string PostalCode { get; set; }
      public string City { get; set; }
      public string Country { get; set; }
      public List<Guid> CareUsersToAdd { get; set; }
      public List<Guid> CareUsersToRemove { get; set; }
   }
}
