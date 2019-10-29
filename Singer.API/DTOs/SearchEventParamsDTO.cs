using System;
using System.ComponentModel;

namespace Singer.DTOs
{
   public class SearchEventParamsDTO
   {
      [DisplayName("Start datum")]
      public DateTime? StartDate { get; set; }

      [DisplayName("Eind datum")]
      public DateTime? EndDate { get; set; }

      [DisplayName("Locatie id")]
      public Guid? LocationId { get; set; }
   }
}
