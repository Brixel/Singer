using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Singer.DTOs;
using Singer.Models;

namespace Singer.Services.Interfaces
{
   public interface IActionNotificationService
   {
      Task<List<EventRegistrationLogDTO>> GetEventRegistrationLogsWaitingForAction();
      Task RegisterEventRegistrationLocationChange(Guid eventRegistrationId,
         RegistrationStatus originalStatus, RegistrationStatus newRegistrationStatus);
      Task RegisterEventRegistrationLocationChange(Guid eventRegistrationId, Guid previousLocationId, Guid newLocationId);
   }
}
