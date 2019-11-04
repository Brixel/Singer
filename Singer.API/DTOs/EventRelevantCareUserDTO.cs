using System;
using Singer.Models;

namespace Singer.DTOs
{
   public class EventRelevantCareUserDTO
   {
      public Guid Id { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public AgeGroup AgeGroup { get; set; }
      public bool AppropriateAgeGroup { get; set; }
   }
}
