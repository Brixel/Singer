using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Singer.DTOs;
using Singer.Models;

namespace Singer.Services.Interfaces
{
   public interface IActionNotificationService
   {
      Task<List<EventRegistrationLogCareUserDTO>> GetEventRegistrationLogsWaitingForAction();
      Task RegisterEventRegistrationStatusChange(Guid eventRegistrationId,
         Guid executedByUserId,
         RegistrationStatus originalStatus, RegistrationStatus newRegistrationStatus);
      Task RegisterEventRegistrationLocationChange(Guid eventRegistrationId,
         Guid executedByUserId, Guid previousLocationId, Guid newLocationId);
   }
}
