using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Singer.Models;

namespace Singer.Services.Interfaces;

public interface IActionNotificationService
{
    Task<List<EventRegistrationLogWrapper>> GetEventRegistrationLogsWaitingForAction(Guid? userId = null);
    Task RegisterEventRegistrationStatusChange(Guid eventRegistrationId,
       Guid executedByUserId,
       RegistrationStatus originalStatus, RegistrationStatus newRegistrationStatus);
    Task RegisterEventRegistrationLocationChange(Guid eventRegistrationId,
       Guid executedByUserId, Guid previousLocationId, Guid newLocationId);

    Task SendEmails(Guid userId);
}
