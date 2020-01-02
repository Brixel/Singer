using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.DTOs;
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

      public async Task<List<EventRegistrationLogDTO>> GetEventRegistrationLogsWaitingForAction()
      {
         var logs = await _context.EventRegistrationLogs.Where(x => !x.ActionTaken())
            .Select(x => new
            {
               Id = x.Id,
               EventRegistrationId  = x.EventRegistrationId,
               CareUser = new
               {
                  x.EventRegistration.CareUser.User.FirstName,
                  x.EventRegistration.CareUser.User.LastName
               },
               LegalGuardians = x.EventRegistration.CareUser.LegalGuardianCareUsers.Select(lc => new
               {
                  lc.LegalGuardian.User.FirstName,
                  lc.LegalGuardian.User.LastName
               }),
               CreationDateTimeUTC = x.CreationDateTimeUTC
            }).ToListAsync();

         var eventRegistrationLogs = logs.Select(x => new EventRegistrationLogDTO()
         {
            Id = x.Id,
            CreationDateTimeUTC = x.CreationDateTimeUTC,
            EventRegistrationId = x.EventRegistrationId,
            CareUser = $"{x.CareUser.FirstName} {x.CareUser.LastName}",
            LegalGuardians = x.LegalGuardians.Select(lc => $"{lc.FirstName} {lc.LastName}").ToList()
         }).ToList();
         return eventRegistrationLogs;
      }
   }
}
