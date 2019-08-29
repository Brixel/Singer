using Singer.DTOs.Users;
using Singer.Models;
using System;

namespace Singer.DTOs
{
   public class CreateEventRegistrationDTO
   {
      public Guid Id { get; set; }
      public Guid EventId { get; set; }
      public CareUserDTO CareUser { get; set; }
      public RegistrationStatus Status { get; set; }
   }
}
