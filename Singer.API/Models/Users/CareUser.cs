using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Singer.Helpers;

namespace Singer.Models.Users
{
   public class CareUser : IUser
   {
      public Guid Id { get; set; }

      [ForeignKey(nameof(User))]
      public Guid UserId { get; set; }

      public User User { get; set; }
      public List<LegalGuardianCareUser> LegalGuardianCareUsers { get; set; }

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

      public List<EventRegistration> EventRegistrations { get; set; }
   }
}
