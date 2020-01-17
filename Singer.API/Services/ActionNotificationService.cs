using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
      private readonly IEmailService _emailService;

      public ActionNotificationService(ApplicationDbContext context, IEmailService emailService)
      {
         _context = context;
         _emailService = emailService;
      }

      /// <summary>
      /// Method to track actions taken on an event registration
      /// If a log exists for the corresponding action, it cancels the action, meaning the log can be deleted
      /// </summary>
      /// <param name="eventRegistrationId"></param>
      /// <param name="executedByUserId"></param>
      /// <param name="previousLocationId"></param>
      /// <param name="newLocationId"></param>
      /// <returns></returns>
      public async Task RegisterEventRegistrationLocationChange(Guid eventRegistrationId,
         Guid executedByUserId, Guid previousLocationId, Guid newLocationId)
      {
         var eventRegistrationLog = EventRegistrationLocationChange.Create
            (eventRegistrationId, executedByUserId, previousLocationId, newLocationId);
         var lastLogForRegistration = _context.EventRegistrationLogs
            .OfType<EventRegistrationLocationChange>()
            .LastOrDefault(x => x.EventRegistrationId == eventRegistrationLog.EventRegistrationId);

         if (lastLogForRegistration != null &&
             lastLogForRegistration.PreviousLocationId == newLocationId)
         {
            _context.EventRegistrationLogs.Remove(lastLogForRegistration);
         }
         else
         {

            await _context.EventRegistrationLogs.AddAsync(eventRegistrationLog);
         }
      }

      public async Task SendEmails(Guid userId)
      {
         var logsAwaitingAction
            = await GetEventRegistrationLogsWaitingForAction(userId);
         foreach (var actionsForCareUser in logsAwaitingAction)
         {
            var careTakers = string.Join(", ", actionsForCareUser.LegalGuardians);
            var emailTemplate = $"Beste {careTakers}<br />Er zijn wijzigingen in de inschrijvingen voor" +
                                $" {actionsForCareUser.CareUser} geweest. Hieronder is een samenvatting:<br /><br />";
            if (actionsForCareUser.RegistrationStateChanges.Any())
            {
               emailTemplate += "Goedkeuringen en afkeuringen<br /><ul>";

               foreach (var registration in actionsForCareUser.RegistrationStateChanges)
               {
                  var newStatus = registration.NewStatus;
                  var statusString = newStatus switch
                  {
                     RegistrationStatus.Accepted => "Goedgekeurd",
                     RegistrationStatus.Rejected => "Afgekeurd",
                     _ => "Afwachting"
                  };
                  emailTemplate += $"<li>{registration.EventTitle} " +
                                   $"van {registration.EventSlotStartDateTime:dd-MM-yyyy HH:mm}" +
                                   $"tot {registration.EventSlotEndDateTime:dd-MM-yyyy HH:mm}:" +
                                   $"{statusString}</li>";
               }

               emailTemplate += "</ul>";
            }

            if (actionsForCareUser.RegistrationLocationChanges.Any())
            {
               emailTemplate += "Locatiewijzigingen<br /><ul>";

               foreach (var registration in actionsForCareUser.RegistrationLocationChanges)
               {
                  emailTemplate += $"<li>{registration.EventTitle} " +
                                   $"van {registration.EventSlotStartDateTime:dd-MM-yyyy HH:mm}" +
                                   $"tot {registration.EventSlotEndDateTime:dd-MM-yyyy HH:mm}:" +
                                   $"{registration.NewLocation}</li>";
               }

               emailTemplate += "</ul>";
            }

            await _emailService.Send("Inschrijvingen", emailTemplate,
               actionsForCareUser.LegalGuardians.Select(x => x.Email).ToList());
         }
      }

      public async Task<List<EventRegistrationLogCareUserDTO>> GetEventRegistrationLogsWaitingForAction(Guid? userId = null)
      {
         var locations = _context.EventLocations
            .Select(x => new {x.Id, x.Name})
            .ToDictionary(x => x.Id, x => x.Name);
         Expression<Func<EventRegistrationStatusChange, bool>> statusChangeExpression;
         Expression<Func<EventRegistrationLocationChange, bool>> locationChangeExpression;
         if (userId.HasValue)
         {
            statusChangeExpression = x => !x.EmailSent && x.ExecutedByUserId == userId;
            locationChangeExpression = x => !x.EmailSent && x.ExecutedByUserId == userId;
         }
         else
         {
            statusChangeExpression = x => !x.EmailSent;
            locationChangeExpression = x => !x.EmailSent;
         }
         var registrationLocationChanges =
            await _context.EventRegistrationLogs
            .OfType<EventRegistrationLocationChange>()
            .Where(locationChangeExpression)
            .Select(x => new
            {
               Id = x.Id,
               EventRegistration = new
               {
                  x.EventRegistrationId,
                  EventTitle = x.EventRegistration.EventSlot.Event.Title,
                  EventSlotStartDateTime = x.EventRegistration.EventSlot.StartDateTime,
                  EventSlotEndDateTime = x.EventRegistration.EventSlot.EndDateTime,
                  NewLocation = locations[x.NewLocationId]
               },
               CareUser = new
               {
                  x.EventRegistration.CareUserId,
                  x.EventRegistration.CareUser.User.FirstName,
                  x.EventRegistration.CareUser.User.LastName
               },
               LegalGuardians = x.EventRegistration.CareUser
                  .LegalGuardianCareUsers.Select(lc => new
               {
                  lc.LegalGuardian.User.FirstName,
                  lc.LegalGuardian.User.LastName,
                  lc.LegalGuardian.User.Email
                  }),
               CreationDateTimeUTC = x.CreationDateTimeUTC
            }).ToListAsync();


         var registrationStatusChanges = await _context.EventRegistrationLogs
            .OfType<EventRegistrationStatusChange>()
            .Where(statusChangeExpression)
            .Select(x => new
            {
               EventRegistration  = new
               {
                  x.EventRegistrationId,
                  EventTitle = x.EventRegistration.EventSlot.Event.Title,
                  EventSlotStartDateTime = x.EventRegistration.EventSlot.StartDateTime,
                  EventSlotEndDateTime = x.EventRegistration.EventSlot.EndDateTime,
                  NewStatus = x.NewStatus
               },
               CareUser = new
               {
                  x.EventRegistration.CareUserId,
                  x.EventRegistration.CareUser.User.FirstName,
                  x.EventRegistration.CareUser.User.LastName
               },
               LegalGuardians = x.EventRegistration.CareUser.LegalGuardianCareUsers.Select(lc => new
               {
                  lc.LegalGuardian.User.FirstName,
                  lc.LegalGuardian.User.LastName,
                  lc.LegalGuardian.User.Email
               }),
               CreationDateTimeUTC = x.CreationDateTimeUTC
            }).ToListAsync();

         var eventRegistrationLogs =
            registrationStatusChanges
               .GroupBy(x => x.CareUser.CareUserId)
               .Select(careUserId =>
                  {
                     var careUser = careUserId.First().CareUser;
                     var legalGuardians = careUserId.First().LegalGuardians
                        .Select(lc => new EventRegistrationLogCareUserDTO.LegalGuardianDTO(){
                           Name = $"{lc.FirstName} {lc.LastName}",
                           Email = lc.Email
                        }).ToList();
                     return new EventRegistrationLogCareUserDTO()
                     {
                        Id = careUserId.Key,
                        CreationDateTimeUTC = careUserId.First().CreationDateTimeUTC,
                        CareUser = $"{careUser.FirstName} {careUser.LastName}",
                        LegalGuardians = legalGuardians,
                        RegistrationStateChanges = careUserId.Select(eventRegistration => new
                           CareUserRegistrationStateChangedDTO()
                           {
                              EventRegistrationId = eventRegistration.EventRegistration.EventRegistrationId,
                              EventTitle = eventRegistration.EventRegistration.EventTitle,
                              EventSlotStartDateTime = eventRegistration.EventRegistration.EventSlotStartDateTime,
                              EventSlotEndDateTime = eventRegistration.EventRegistration.EventSlotStartDateTime,
                              NewStatus = eventRegistration.EventRegistration.NewStatus
                           }).ToList()

                     };
                  }).ToList();

         var eventRegistrationLocations =
            registrationLocationChanges
               .GroupBy(x => x.CareUser.CareUserId)
               .Select(careUserId =>
               {
                  var careUser = careUserId.First().CareUser;
                  var legalGuardians = careUserId.First().LegalGuardians
                     .Select(lc => new EventRegistrationLogCareUserDTO.LegalGuardianDTO(){
                        Name = $"{lc.FirstName} {lc.LastName}",
                        Email = lc.Email
                     }).ToList();
                  return new EventRegistrationLogCareUserDTO()
                  {
                     Id = careUserId.Key,
                     CreationDateTimeUTC = careUserId.First().CreationDateTimeUTC,
                     CareUser = $"{careUser.FirstName} {careUser.LastName}",
                     LegalGuardians = legalGuardians,
                     RegistrationLocationChanges = careUserId.Select(eventRegistration => new
                        CareUserRegistrationLocationChangedDTO()
                        {
                           EventRegistrationId = eventRegistration.EventRegistration.EventRegistrationId,
                           EventTitle = eventRegistration.EventRegistration.EventTitle,
                           EventSlotStartDateTime = eventRegistration.EventRegistration.EventSlotStartDateTime,
                           EventSlotEndDateTime = eventRegistration.EventRegistration.EventSlotStartDateTime,
                           NewLocation = eventRegistration.EventRegistration.NewLocation
                        }).ToList()
                  };
               }).ToList();

         foreach (var eventRegistrationLogCareUserDto in eventRegistrationLogs)
         {
            var careUserId = eventRegistrationLocations
               .SingleOrDefault(x => x.Id == eventRegistrationLogCareUserDto.Id);
            if (careUserId != null)
            {
               eventRegistrationLogCareUserDto.RegistrationLocationChanges = careUserId.RegistrationLocationChanges;
            }
         }

         var processedCareUserIds = eventRegistrationLogs.Select(x => x.Id).ToList();

         var remainingCareUsers =
            eventRegistrationLocations.Where(x => !processedCareUserIds.Contains(x.Id))
               .ToList();
         eventRegistrationLogs.AddRange(remainingCareUsers);
         return eventRegistrationLogs;
      }

      public async Task RegisterEventRegistrationStatusChange(Guid eventRegistrationId, Guid executedByUserId,
         RegistrationStatus previousStatus, RegistrationStatus newRegistrationStatus)
      {
         var eventRegistrationLog = EventRegistrationStatusChange.Create(eventRegistrationId, executedByUserId, previousStatus, newRegistrationStatus);

         var lastLogForRegistration = _context.EventRegistrationLogs
            .OfType<EventRegistrationStatusChange>()
            .LastOrDefault(x => x.EventRegistrationId == eventRegistrationLog.EventRegistrationId);

         if (lastLogForRegistration != null &&
             lastLogForRegistration.PreviousStatus == newRegistrationStatus)
         {
            _context.EventRegistrationLogs.Remove(lastLogForRegistration);
         }
         else
         {

            await _context.EventRegistrationLogs.AddAsync(eventRegistrationLog);
         }
      }
   }
}
