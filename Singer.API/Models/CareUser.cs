using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Singer.Models
{
   public class CareUser
   {
      public Guid Id { get; set; }

      [ForeignKey(nameof(User))]
      public Guid UserId { get; set; }

      public User User { get; set; }

      [PersonalData]
      public DateTime BirthDay { get; set; }

      [PersonalData]
      public string CaseNumber { get; set; }

      [PersonalData]
      public AgeGroup AgeGroup { get; set; }

      [PersonalData]
      public bool IsExtern { get; set; }

      [PersonalData]
      public bool HasTrajectory { get; set; }

      [PersonalData]
      public bool HasNormalDayCare { get; set; }

      [PersonalData]
      public bool HasVacationDayCare { get; set; }

      [PersonalData]
      public bool HasResources { get; set; }
   }
}