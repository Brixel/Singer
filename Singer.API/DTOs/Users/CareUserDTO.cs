using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Singer.Models;

namespace Singer.DTOs.Users
{
   public class CareUserDTO : UserDTO
   {
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
      public EventLocation NormalDaycareLocation { get; set; }

      [DisplayName("Opv. vakantie")]
      public EventLocation VacationDaycareLocation { get; set; }

      [DisplayName("Middelen")]
      public bool HasResources { get; set; }
      public List<LinkedLegalGuardianDTO> LegalGuardianUsers { get; set; }
   }


   public class CreateCareUserDTO : CreateUserDTO
   {
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
      public Guid? NormalDaycareLocationId { get; set; }

      [DisplayName("Opv. vakantie")]
      public Guid? VacationDaycareLocationId { get; set; }

      [DisplayName("Middelen")]
      public bool HasResources { get; set; }
   }

   public class UpdateCareUserDTO : UpdateUserDTO
   {
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
      public Guid? NormalDaycareLocationId { get; set; }

      [DisplayName("Opv. vakantie")]
      public Guid? VacationDaycareLocationId { get; set; }

      [DisplayName("Middelen")]
      public bool HasResources { get; set; }
      public List<Guid> LegalGuardianUsersToAdd { get; set; }
      public List<Guid> LegalGuardianUsersToRemove { get; set; }
   }
}
