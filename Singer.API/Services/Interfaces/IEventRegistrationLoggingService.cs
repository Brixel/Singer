using System;
using System.Threading.Tasks;
using Singer.DTOs;
using Singer.Models;

namespace Singer.Services.Interfaces
{
   public interface IEventRegistrationLoggingService
   {
      Task LogEventRegistration(Guid eventRegistrationId, EventRegistrationChanges registrationChange);

      Task<EventRegistrationLogDTO> GetEventRegistrationLogsWaitingForAction();
   }
}
