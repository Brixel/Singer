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

      [DisplayName("Voornaam")]
      public string FirstName { get; set; }


      [DisplayName("Achternaam")]
      public string LastName { get; set; }

      public string Email { get; set; }
   }
}
