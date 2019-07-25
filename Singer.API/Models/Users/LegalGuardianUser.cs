using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Singer.Models.Users
{
   public class LegalGuardianUser : IUser
   {
      public Guid Id { get; set; }

      [ForeignKey(nameof(User))]
      public Guid UserId { get; set; }
      public User User { get; set; }
      public List<LegalGuardianCareUser> LegalGuardianCareUsers { get; set; }
      public string Address { get; set; }
      public string PostalCode { get; set; }
      public string City { get; set; }
      public string Country { get; set; }
   }
}
