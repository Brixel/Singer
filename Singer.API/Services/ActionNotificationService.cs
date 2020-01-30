using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.ResponseCaching.Internal;
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
         // Only process actions having legal guardians
         foreach (var actionsForCareUser in logsAwaitingAction
            .Where(x => x.CareUserLogDTO.LegalGuardians.Any()))
         {
            var registrationLogIds = actionsForCareUser.RegistrationLogIds;
            var careUserDTO = actionsForCareUser.CareUserLogDTO;
            var careTakers = string.Join(", ",
               careUserDTO.LegalGuardians
                  .Select(lg => lg.Name));
            var ccEmails = actionsForCareUser.ExecutedByEmails;
            var emailTemplate = $"Beste {careTakers}<br />Er zijn wijzigingen in de inschrijvingen voor" +
                                $" {careUserDTO.CareUser} geweest. Hieronder is een samenvatting:<br /><br />";
            if (careUserDTO.RegistrationStateChanges.Any() && careUserDTO.LegalGuardians.Any())
            {
               emailTemplate += "Goedkeuringen en afkeuringen<br /><ul>";

               foreach (var registration in careUserDTO.RegistrationStateChanges)
               {
                  var newStatus = registration.NewStatus;
                  var statusString = newStatus switch
                  {
                     RegistrationStatus.Accepted => "Goedgekeurd",
                     RegistrationStatus.Rejected => "Afgekeurd",
                     _ => "Afwachting"
                  };
                  emailTemplate += GetEmailTemplateForEventSlot(registration.EventTitle,
                     registration.EventSlotStartDateTime, registration.EventSlotEndDateTime, statusString);
               }

               emailTemplate += "</ul>";
            }

            if (careUserDTO.RegistrationLocationChanges.Any())
            {
               emailTemplate += "Locatiewijzigingen<br /><ul>";

               foreach (var registration in careUserDTO.RegistrationLocationChanges)
               {
                  emailTemplate += GetEmailTemplateForEventSlot(registration.EventTitle,
                     registration.EventSlotStartDateTime, registration.EventSlotEndDateTime,
                     registration.NewLocation);
               }

               emailTemplate += "</ul>";
            }

            emailTemplate += "<br />Met vriendelijke groeten,<br /><br />Sint Gerardus";

            await _emailService.Send("Inschrijvingen", emailTemplate,
               careUserDTO.LegalGuardians.Select(x => x.Email).ToList(),
               ccEmails);
            MarkActionNotificationsForThisCareUserAsSent(registrationLogIds);
         }
      }

      private void MarkActionNotificationsForThisCareUserAsSent(List<Guid> registrationLogIds)
      {
         var registrationLogsToBeProcessed = _context.EventRegistrationLogs
            .Where(x => registrationLogIds.Contains(x.Id)).ToList();
         foreach (var eventRegistrationLog in registrationLogsToBeProcessed)
         {
            eventRegistrationLog.EmailSent = true;
         }

      }

      private static string GetEmailTemplateForEventSlot(string eventTitle,
         DateTime eventSlotStartDateTime,
         DateTime eventSlotEndDateTime, string newValue)
      {
         return $"<li>{eventTitle} " +
                $"van {eventSlotStartDateTime:dd-MM-yyyy HH:mm} " +
                $"tot {eventSlotEndDateTime:dd-MM-yyyy HH:mm}: " +
                $"{newValue}</li>";
      }

      public async Task<List<EventRegistrationLogWrapper>> GetEventRegistrationLogsWaitingForAction(Guid? userId = null)
      {
         var locations = _context.EventLocations
            .Select(x => new {x.Id, x.Name})
            .ToDictionary(x => x.Id, x => x.Name);
         Expression<Func<EventRegistrationStatusChange, bool>> statusChangeExpression;
         Expression<Func<EventRegistrationLocationChange, bool>> locationChangeExpression;
         if (userId.HasValue)
         {
            statusChangeExpression = x =>
               !x.EmailSent &&
               x.ExecutedByUserId == userId &&
               x.EventRegistration.CareUser.LegalGuardianCareUsers.Any();
            locationChangeExpression = x =>
               !x.EmailSent &&
               x.ExecutedByUserId == userId &&
               x.EventRegistration.CareUser.LegalGuardianCareUsers.Any(); ;
         }
         else
         {
            statusChangeExpression = x =>
               !x.EmailSent &&
               x.EventRegistration.CareUser.LegalGuardianCareUsers.Any();
            locationChangeExpression = x =>
               !x.EmailSent &&
               x.EventRegistration.CareUser.LegalGuardianCareUsers.Any();
         }
         var registrationLocationChanges =
            await _context.EventRegistrationLogs
            .OfType<EventRegistrationLocationChange>()
            .Where(locationChangeExpression)
            .Select(x => new
            {
               RegistrationLogId = x.Id,
               ExecutedByEmail = x.ExecutedByUser.Email,
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


         var registrationStatusChanges =
            await _context.EventRegistrationLogs
            .OfType<EventRegistrationStatusChange>()
            .Where(statusChangeExpression)
            .Select(x => new
            {
               RegistrationLogId = x.Id,
               ExecutedByEmail = x.ExecutedByUser.Email,
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
                     var registrationLogIds =
                        careUserId.Select(x => x.RegistrationLogId).ToList();
                     var executedByEmail =
                        careUserId.Select(c => c.ExecutedByEmail)
                           .Distinct().ToList();
                     var logDTO = new EventRegistrationLogCareUserDTO()
                     {
                        CareUserId = careUserId.Key,
                        CreationDateTimeUTC = careUserId.First().CreationDateTimeUTC,
                        CareUser = $"{careUser.FirstName} {careUser.LastName}",
                        LegalGuardians = legalGuardians,
                        RegistrationStateChanges = careUserId.Select(eventRegistration => new
                           CareUserRegistrationStateChangedDTO()
                           {
                              EventRegistrationId = eventRegistration.EventRegistration.EventRegistrationId,
                              EventTitle = eventRegistration.EventRegistration.EventTitle,
                              EventSlotStartDateTime = eventRegistration.EventRegistration.EventSlotStartDateTime,
                              EventSlotEndDateTime = eventRegistration.EventRegistration.EventSlotEndDateTime,
                              NewStatus = eventRegistration.EventRegistration.NewStatus
                           }).ToList()

                     };
                     return EventRegistrationLogWrapper.Create(careUserId.Key, registrationLogIds, logDTO, executedByEmail);
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
                  var registrationLogIds =
                     careUserId.Select(x => x.RegistrationLogId).ToList();
                  var executedByEmail =
                     careUserId.Select(c => c.ExecutedByEmail)
                        .Distinct().ToList();
                  var logDTO = new EventRegistrationLogCareUserDTO()
                  {
                     CareUserId = careUserId.Key,
                     CreationDateTimeUTC = careUserId.First().CreationDateTimeUTC,
                     CareUser = $"{careUser.FirstName} {careUser.LastName}",
                     LegalGuardians = legalGuardians,
                     RegistrationLocationChanges = careUserId.Select(eventRegistration => new
                        CareUserRegistrationLocationChangedDTO()
                        {
                           EventRegistrationId = eventRegistration.EventRegistration.EventRegistrationId,
                           EventTitle = eventRegistration.EventRegistration.EventTitle,
                           EventSlotStartDateTime = eventRegistration.EventRegistration.EventSlotStartDateTime,
                           EventSlotEndDateTime = eventRegistration.EventRegistration.EventSlotEndDateTime,
                           NewLocation = eventRegistration.EventRegistration.NewLocation
                        }).ToList()
                  };
                  return EventRegistrationLogWrapper.Create(careUserId.Key, registrationLogIds, logDTO, executedByEmail);
               }).ToList();

         foreach (var eventRegistrationLogCareUserDto in eventRegistrationLogs)
         {
            var careUserId = eventRegistrationLocations
               .SingleOrDefault(x =>
                  x.CareUserId == eventRegistrationLogCareUserDto.CareUserId);
            if (careUserId != null)
            {
               eventRegistrationLogCareUserDto.CareUserLogDTO.RegistrationLocationChanges = careUserId.CareUserLogDTO.RegistrationLocationChanges;
               eventRegistrationLogCareUserDto.AddLogIds(eventRegistrationLogCareUserDto.RegistrationLogIds);
            }
         }

         var processedCareUserIds =
            eventRegistrationLogs.Select(x => x.CareUserId).ToList();

         var remainingCareUsers =
            eventRegistrationLocations.Where(x => !processedCareUserIds.Contains(x.CareUserId))
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

   public class EventRegistrationLogWrapper
   {
      private EventRegistrationLogWrapper(Guid careUserId, List<Guid> registrationLogIds,
         EventRegistrationLogCareUserDTO careUserLogDto, List<string> executedByEmails)
      {
         CareUserId = careUserId;
         RegistrationLogIds = registrationLogIds;
         CareUserLogDTO = careUserLogDto;
         ExecutedByEmails = executedByEmails;
      }

      public Guid CareUserId { get; }
      public List<Guid> RegistrationLogIds { get; }

      public List<string> ExecutedByEmails { get; }
      public EventRegistrationLogCareUserDTO CareUserLogDTO { get; }

      public static EventRegistrationLogWrapper Create(Guid careUserId, List<Guid> registrationLogIds,
         EventRegistrationLogCareUserDTO careUserLogDTO, List<string> executedByEmail)
      {
         return new EventRegistrationLogWrapper(careUserId, registrationLogIds, careUserLogDTO, executedByEmail);
      }

      public void AddLogIds(IReadOnlyList<Guid> registrationLogIds)
      {
         RegistrationLogIds.AddRange(registrationLogIds);
      }
   }
}
