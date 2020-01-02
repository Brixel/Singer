using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.Models;
using Singer.Services.Interfaces;

namespace Singer.Services
{
   class EventRegistrationLoggingService : IEventRegistrationLoggingService
   {
      private readonly ApplicationDbContext _context;

      public EventRegistrationLoggingService(ApplicationDbContext context)
      {
         _context = context;
      }

      public async Task LogEventRegistration(Guid eventRegistrationId, EventRegistrationChanges registrationChange)
      {
         var eventRegistrationLog = EventRegistrationLog.Create(eventRegistrationId, registrationChange);
         await _context.AddAsync(eventRegistrationLog);
      }
   }
}
