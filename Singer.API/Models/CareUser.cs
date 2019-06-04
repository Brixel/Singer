using System;
using Microsoft.AspNetCore.Identity;

namespace Singer.Models
{
   public class CareUser : User
   {
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

   public enum AgeGroup
   {
      Toddler,
      Child,
      Youngster,
      Adult
   }
}
