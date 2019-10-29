using Singer.Helpers;
using Singer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Singer.DTOs
{
   public class EventDTO : IIdentifiable
   {
      [DisplayName("Id")]
      public Guid Id { get; set; }

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
      [DisplayName("Leeftijdsgroepen")]
      public List<AgeGroup> AllowedAgeGroups { get; set; }

      [Required]
      [DisplayName("Locatie")]
      public EventLocationDTO Location { get; set; }

      [Required]
      [Range(1, int.MaxValue)]
      [DisplayName("Maximum aantal inschrijvingen")]
      public int MaxRegistrants { get; set; }

      [Required]
      [Range(0, int.MaxValue)]
      [DisplayName("Aantal inschrijvingen")]
      public int CurrentRegistrants { get; set; }

      [Required]
      [Range(0, int.MaxValue)]
      [DisplayName("Prijs")]
      public decimal Cost { get; set; }

      [Required]
      [DisplayName("Start datum")]
      public DateTime StartDateTime { get; set; }

      [Required]
      [DisplayName("Eind datum")]
      public DateTime EndDateTime { get; set; }

      [Required]
      [DisplayName("Start registratie datum")]
      public DateTime StartRegistrationDateTime { get; set; }

      [Required]
      [DisplayName("Eind registratie datum")]
      public DateTime EndRegistrationDateTime { get; set; }

      [Required]
      [DisplayName("Uiterste annuleringsdatum")]
      public DateTime FinalCancellationDateTime { get; set; }

      [Required]
      [DisplayName("Registratie op dagelijkse basis")]
      public bool RegistrationOnDailyBasis { get; set; }

      [Required]
      [DisplayName("Opvang voor het evenement")]
      public bool HasDayCareBefore { get; set; }

      [DisplayName("Start opvang voor het evenement")]
      public DateTime? DayCareBeforeStartDateTime { get; set; }

      [Required]
      [DisplayName("Opvan na het evenement")]
      public bool HasDayCareAfter { get; set; }

      [DisplayName("Einde opvang na het evenement")]
      public DateTime? DayCareAfterEndDateTime { get; set; }

      [Required]
      [DisplayName("Event slots")]
      public IList<EventSlotDTO> EventSlots { get; set; }
   }

   public class CreateEventDTO
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
      [DisplayName("Leeftijdsgroepen")]
      public List<AgeGroup> AllowedAgeGroups { get; set; }

      [Required]
      [DisplayName("Locatie id")]
      public Guid LocationId { get; set; }

      [Required]
      [Range(1, int.MaxValue)]
      [DisplayName("Maximum aantal inschrijvingen")]
      public int MaxRegistrants { get; set; }

      [Required]
      [Range(0, int.MaxValue)]
      [DisplayName("Prijs")]
      public decimal Cost { get; set; }

      [Required]
      [DisplayName("Start datum")]
      public DateTime StartDateTime { get; set; }

      [Required]
      [DisplayName("Eind datum")]
      public DateTime EndDateTime { get; set; }

      [Required]
      [DisplayName("Start registratie datum")]
      public DateTime StartRegistrationDateTime { get; set; }

      [Required]
      [DisplayName("Eind registratie datum")]
      public DateTime EndRegistrationDateTime { get; set; }

      [Required]
      [DisplayName("Uiterste annuleringsdatum")]
      public DateTime FinalCancellationDateTime { get; set; }

      [Required]
      [DisplayName("Registratie op dagelijkse basis")]
      public bool RegistrationOnDailyBasis { get; set; }

      [Required]
      [DisplayName("Opvang voor het evenement")]
      public bool HasDayCareBefore { get; set; }

      [DisplayName("Start opvang voor het evenement")]
      public DateTime? DayCareBeforeStartDateTime { get; set; }

      [Required]
      [DisplayName("Opvan na het evenement")]
      public bool HasDayCareAfter { get; set; }

      [DisplayName("Einde opvang na het evenement")]
      public DateTime? DayCareAfterEndDateTime { get; set; }

      [DisplayName("Herhaling")]
      public RepeatSettings RepeatSettings { get; set; }
   }

   public class UpdateEventDTO
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
      [DisplayName("Leeftijdsgroepen")]
      public List<AgeGroup> AllowedAgeGroups { get; set; }

      [Required]
      [DisplayName("Locatie id")]
      public string LocationId { get; set; }

      [Required]
      [Range(1, int.MaxValue)]
      [DisplayName("Maximum aantal inschrijvingen")]
      public int MaxRegistrants { get; set; }

      [Required]
      [Range(0, int.MaxValue)]
      [DisplayName("Prijs")]
      public decimal Cost { get; set; }

      [Required]
      [DisplayName("Start datum")]
      public DateTime StartDateTime { get; set; }

      [Required]
      [DisplayName("Eind datum")]
      public DateTime EndDateTime { get; set; }

      [Required]
      [DisplayName("Start registratie datum")]
      public DateTime StartRegistrationDateTime { get; set; }

      [Required]
      [DisplayName("Eind registratie datum")]
      public DateTime EndRegistrationDateTime { get; set; }

      [Required]
      [DisplayName("Uiterste annuleringsdatum")]
      public DateTime FinalCancellationDateTime { get; set; }

      [Required]
      [DisplayName("Registratie op dagelijkse basis")]
      public bool RegistrationOnDailyBasis { get; set; }

      [Required]
      [DisplayName("Opvang voor het evenement")]
      public bool HasDayCareBefore { get; set; }

      [DisplayName("Start opvang voor het evenement")]
      public DateTime? DayCareBeforeStartDateTime { get; set; }

      [Required]
      [DisplayName("Opvan na het evenement")]
      public bool HasDayCareAfter { get; set; }

      [DisplayName("Einde opvang na het evenement")]
      public DateTime? DayCareAfterEndDateTime { get; set; }
   }
}
