using System;

namespace Singer.Models.Users;

public class LegalGuardianCareUser
{
    public Guid LegalGuardianId { get; set; }
    public virtual LegalGuardianUser LegalGuardian { get; set; }
    public Guid CareUserId { get; set; }
    public virtual CareUser CareUser { get; set; }
}
