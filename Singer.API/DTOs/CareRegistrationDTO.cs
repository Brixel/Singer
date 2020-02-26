using System;
using System.Collections.Generic;
using Singer.Models;

namespace Singer.DTOs
{

   public class CreateCareRegistrationDTO
   {
      public IReadOnlyList<Guid> CareUserIds { get; set; }
      public DateTimeOffset StartDateTime { get; set; }
      public DateTimeOffset EndDateTime { get; set; }
      public RegistrationTypes EventRegistrationType { get; set; }
   }

   public class CareRegistrationResultDTO
   {
      public IReadOnlyList<Guid> CreatedRegistrationIds { get; set; }
   }
}
