using System;

namespace Singer.DTOs
{
   public class SearchEventParamsDTO
   {
      public DateTime? StartDate { get; set; }
      public DateTime? EndDate { get; set; }
      public Guid? LocationId { get; set; }
   }
}
