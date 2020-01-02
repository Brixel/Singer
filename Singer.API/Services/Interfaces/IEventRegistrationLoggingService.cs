using System;
using System.Threading.Tasks;
using Singer.Models;

namespace Singer.Services.Interfaces
{
   public interface IEventRegistrationLoggingService
   {
      Task LogEventRegistration(Guid eventRegistrationId, EventRegistrationChanges registrationChange);
   }
}
