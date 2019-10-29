using Singer.Helpers;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs
{
   public class EventSlotDTO : IIdentifiable
   {
      [Required]
      [DisplayName("Id")]
      public Guid Id { get; set; }

      [Required]
      [DisplayName("Start")]
      public DateTime StartDateTime { get; set; }

      [Required]
      [DisplayName("Einde")]
      public DateTime EndDateTime { get; set; }
   }
}
