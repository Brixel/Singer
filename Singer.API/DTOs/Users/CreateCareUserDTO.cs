using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Singer.Models;

namespace Singer.DTOs.Users
{
   public class CreateCareUserDTO : ICreateUserDTO
   {
      [Required]
      [DataType(DataType.Date)]
      [DisplayName("Geboortedatum")]
      public DateTime BirthDay { get; set; }

      [Required]
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

      [Required]
      [StringLength(maximumLength: 255,
         ErrorMessage = "De {0} moet een lengte hebben van minstens {2} en maximum {1} karakters.",
         MinimumLength = 3)]
      [DisplayName("Voornaam")]
      public string FirstName { get; set; }
      [Required]
      [StringLength(maximumLength: 255,
         ErrorMessage = "De {0} moet een lengte hebben van minstens {2} en maximum {1} karakters.",
         MinimumLength = 3)]
      [DisplayName("Achternaam")]
      public string LastName { get; set; }

      // This field has no validation attributes because the careuser has no email
      public string Email { get; set; }
   }
}
