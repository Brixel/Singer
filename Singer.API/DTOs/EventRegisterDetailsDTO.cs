using System;
using System.Collections.Generic;

using Singer.Models;

namespace Singer.DTOs;

public class EventRegisterDetailsDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public IReadOnlyList<AgeGroup> AgeGroups { get; set; }
    public IList<EventSlotRegistrationsDTO> EventSlots { get; set; }
    public IList<EventRelevantCareUserDTO> RelevantCareUsers { get; set; }
    public bool RegistrationOnDailyBasis { get; set; }
}
