using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Singer.DTOs
{
   public class AdminDTO
   {
      public Guid Id { get; set; }
      public string UserName { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Email { get; set; }
   }
}
