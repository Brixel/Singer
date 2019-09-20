using System.Collections.Generic;

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
}
