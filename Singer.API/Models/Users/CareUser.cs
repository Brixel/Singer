using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

namespace Singer.Models.Users;

public class CareUser : IUser
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public virtual User User { get; set; }
    public virtual List<LegalGuardianCareUser> LegalGuardianCareUsers { get; set; }

    [PersonalData]
    public DateTime BirthDay { get; set; }

    [PersonalData]
    public AgeGroup AgeGroup { get; set; }

    [PersonalData]
    public bool IsExtern { get; set; }

    [PersonalData]
    public bool HasTrajectory { get; set; }

    public virtual List<Registration> EventRegistrations { get; set; }
}
