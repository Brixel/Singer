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
   class ActionNotificationService : IActionNotificationService
   {
      private readonly ApplicationDbContext _context;

      public ActionNotificationService(ApplicationDbContext context)
      {
         _context = context;
      }

      /// <summary>
      /// Method to track actions taken on an event registration
      /// If a log exists for the corresponding action, it cancels the action, meaning the log can be deleted
      /// </summary>
      /// <param name="eventRegistrationId"></param>
      /// <param name="registrationChange"></param>
      /// <returns></returns>
      public async Task RegisterEventRegistrationLocationChange(Guid eventRegistrationId, Guid previousLocationId, Guid newLocationId)
      {
         var eventRegistrationLog = EventRegistrationLocationChange.Create
            (eventRegistrationId, previousLocationId, newLocationId);
         await _context.AddAsync(eventRegistrationLog);
      }

      public async Task<List<EventRegistrationLogDTO>> GetEventRegistrationLogsWaitingForAction()
      {
         var logs = await _context.EventRegistrationLocationChanges.Where(x => !x.ActionTaken())
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

      public async Task RegisterEventRegistrationLocationChange(Guid eventRegistrationId,
         RegistrationStatus previousStatus, RegistrationStatus newRegistrationStatus)
      {
         var eventRegistrationLog = EventRegistrationStatusChange.Create(eventRegistrationId, previousStatus, newRegistrationStatus);
         await _context.AddAsync(eventRegistrationLog);
      }
   }
}
