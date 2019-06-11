using System;
using System.ComponentModel;

namespace Singer.DTOs
{
   public class CareUserDTO : CreateCareUserDTO, IUserDTO
   {
      [DisplayName("Id")]
      public Guid Id { get; set; }
   }
}
