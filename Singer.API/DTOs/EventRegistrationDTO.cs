using System;
using Singer.DTOs.Users;
using Singer.Helpers;
using Singer.Models;

namespace Singer.DTOs
{
   public class EventRegistrationDTO : IIdentifiable
   {
      public Guid Id { get; set; }
      public Guid EventId { get; set; }
      public CareUserDTO CareUser { get; set; }
      public RegistrationStatus Status { get; set; }
   }
}
