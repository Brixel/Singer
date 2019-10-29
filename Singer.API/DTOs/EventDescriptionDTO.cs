using Singer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs
{
   public class EventDescriptionDTO
   {
      [Required]
      [StringLength(maximumLength: 100,
          ErrorMessage = "De {0} moet een lengte hebben van minstens {2} en maximum {1} karakters.",
          MinimumLength = 3)]
      [DisplayName("Titel")]
      public string Title { get; set; }

      [StringLength(maximumLength: 1000,
           ErrorMessage = "De {0} moet een lengte hebben van minstens {2} en maximum {1} karakters.",
           MinimumLength = 3)]
      [DisplayName("Beschrijving")]
      public string Description { get; set; }

      [Required]
      [DisplayName("Start datum")]
      public DateTime StartDate { get; set; }

      [Required]
      [DisplayName("Eind datum")]
      public DateTime EndDate { get; set; }

      [Required]
      [DisplayName("Leeftijdsgroepen")]
      public IReadOnlyList<AgeGroup> AgeGroups { get; set; }
   }
}
