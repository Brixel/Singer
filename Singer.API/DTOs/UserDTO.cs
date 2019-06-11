using System;
using System.ComponentModel;

namespace Singer.DTOs
{
   public class UserDTO : CreateUserDTO, IUserDTO
   {
      [DisplayName("Id")]
      public Guid Id { get; set; }
   }
}
