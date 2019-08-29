using System;
using Singer.Helpers;

namespace Singer.Models
{
   public class EventRegistration : IIdentifiable
   {
      public Guid Id { get; set; }
      public Guid EventId { get; set; }
      public Guid UserId { get; set; }

      public RegistrationStatus Status { get; set; }
   }
}
