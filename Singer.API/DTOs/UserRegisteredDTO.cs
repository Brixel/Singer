using System;

using Singer.Models;

namespace Singer.DTOs;

public class UserRegisteredDTO
{
    public Guid CareUserId { get; set; }
    public int PendingStatesRemaining { get; set; }
    public RegistrationStatus Status { get; set; }
    public bool IsRegisteredForAllEventslots { get; set; }
}
