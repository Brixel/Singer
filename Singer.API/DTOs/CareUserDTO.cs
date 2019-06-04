using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Singer.Models;

namespace Singer.DTOs
{
   public class CareUserDTO
   {
      [Required]
      [StringLength(maximumLength: 255,
         ErrorMessage = "De {0} moet een lengte hebben van minstens {2} en maximum {1} karakters.",
         MinimumLength = 3)]
      [DisplayName("Naam")]
      public string Name { get; set; }

      [Required]
      [EmailAddress]
      [DisplayName("E-mail address")]
      public string Email { get; set; }

      [Required]
      [StringLength(maximumLength: 50,
         ErrorMessage = "De {0} moet een lengte hebben van minstens {2} en maximum {1} karakters.",
         MinimumLength = 3)]
      [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "De {0} mag alleen letters en cijfers bevatten.")]
      [DisplayName("Gebruikersnaam")]
      public string UserName { get; set; }

      [Required]
      [DataType(DataType.Date)]
      [DisplayName("Geboortedatum")]
      public DateTime BirthDay { get; set; }

      [Required]
      [StringLength(maximumLength: 10,
         ErrorMessage = "Het {0} moet een lengte hebben van {2} karakters.",
         MinimumLength = 10)]
      [DisplayName("Dossiernummer")]
      public string CaseNumber { get; set; }

      [Required]
      [DisplayName("Leeftijdsgroep")]
      public AgeGroup AgeGroup { get; set; }

      [DisplayName("Klas/Extern")]
      public bool IsExtern { get; set; }

      [DisplayName("Traject")]
      public bool HasTrajectory { get; set; }

      [DisplayName("Opv. normaal")]
      public bool HasNormalDayCare { get; set; }

      [DisplayName("Opv. vakantie")]
      public bool HasVacationDayCare { get; set; }

      [DisplayName("Middelen")]
      public bool HasResources { get; set; }
   }
}
