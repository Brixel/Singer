﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Singer.Data;
using Singer.DTOs;
using Singer.DTOs.Csv;
using Singer.DTOs.Users;
using Singer.Helpers.Exceptions;
using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Profiles;
using Singer.Resources;
using Singer.Services.Interfaces;

namespace Singer.Services;

public class EventRegistrationService : IEventRegistrationService
{
    private readonly IActionNotificationService _actionNotificationService;

    public EventRegistrationService(ApplicationDbContext context, IMapper mapper,
       IActionNotificationService actionNotificationService)
    {
        _actionNotificationService = actionNotificationService;
        Context = context;
        Mapper = mapper;
    }

    protected ApplicationDbContext Context { get; }
    protected IMapper Mapper { get; }

    protected DbSet<Registration> DbSet =>
       Context.Registrations;
    protected IQueryable<Registration> Queryable => Context.Registrations
       .Include(x => x.CareUser).ThenInclude(x => x.User)
       .Include(x => x.EventSlot).ThenInclude(x => x.Event)
       .AsQueryable();

    public Expression<Func<Registration, RegistrationDTO>> Projector => x => new RegistrationDTO
    {
        Id = x.Id,
        Status = x.Status,
        CareUser = new CareUserDTO()
        {
            Id = x.CareUserId,
            AgeGroup = x.CareUser.AgeGroup,
            BirthDay = x.CareUser.BirthDay,
            FirstName = x.CareUser.User.FirstName,
            LastName = x.CareUser.User.LastName
        },
        EventDescription = new EventDescriptionDTO
        {
            Id = x.EventSlot.Event.Id,
            AgeGroups = EventProfile.ToAgeGroupList(x.EventSlot.Event.AllowedAgeGroups),
            Description = x.EventSlot.Event.Description,
            Title = x.EventSlot.Event.Title,
            StartDateTime = x.EventSlot.Event.EventSlots.OrderBy(y => y.StartDateTime).First().StartDateTime,
            EndDateTime = x.EventSlot.Event.EventSlots.OrderByDescending(y => y.EndDateTime).First().EndDateTime
        },
        EventSlot = new EventSlotDTO
        {
            StartDateTime = x.EventSlot.StartDateTime,
            EndDateTime = x.EventSlot.EndDateTime,
            Id = x.EventSlot.Id
        },
    };

    protected static Expression<Func<Registration, bool>> Filter(string filter)
    {
        return o =>
           o.CareUser.User.FirstName.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
           o.CareUser.User.LastName.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
           o.EventSlot.Event.Location.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
           o.EventSlot.Event.Title.Contains(filter, StringComparison.InvariantCultureIgnoreCase);
    }

    public async Task<IReadOnlyList<Guid>> Create(RegistrationTypes registrationType,
       IReadOnlyList<Guid> careUserIds,
       DateTime startDateTime, DateTime endDateTime)
    {
        var registrations = new List<Registration>();
        foreach (var careUserId in careUserIds)
        {
            var duration = endDateTime - startDateTime;

            var initialDateBegin = startDateTime;
            var initialDateEnd = initialDateBegin.SetTime(endDateTime);
            if (registrationType == RegistrationTypes.DayCare)
            {
                GenerateCareRegistrations(registrationType, duration, initialDateBegin, registrations, careUserId, initialDateEnd);
            }
            else
            {
                registrations.Add(Registration.Create(registrationType, careUserId,
                   initialDateBegin, endDateTime));
            }

        }
        await Context.AddRangeAsync(registrations);
        await Context.SaveChangesAsync();
        return registrations.Select(x => x.Id).ToList().AsReadOnly();
    }

    private static void GenerateCareRegistrations(RegistrationTypes registrationType, TimeSpan duration,
       DateTime initialDateBegin, List<Registration> registrations, Guid careUserId, DateTime initialDateEnd)
    {
        var durationDays = duration.Days + 1;
        for (var i = 0; i < durationDays; i++)
        {
            if (initialDateBegin.DayOfWeek is not DayOfWeek.Saturday and not DayOfWeek.Sunday)
            {
                registrations.Add(Registration.Create(registrationType, careUserId, initialDateBegin, initialDateEnd));
            }

            initialDateBegin = initialDateBegin.AddDays(1);
            initialDateEnd = initialDateEnd.AddDays(1);
        }
    }

    public async Task<IReadOnlyList<RegistrationDTO>> CreateAsync(CreateRegistrationDTO dto)
    {
        if (dto == null)
            throw new BadInputException("Input to create registration cannot be null", ErrorMessages.NoDataPassed);

        var eventSlots = Context.EventSlots
           .Where(x => x.EventId == dto.EventId)
           .Select(x => new
           {
               EventSlotId = x.Id,
               StartDateTime = x.StartDateTime,
               EndDateTime = x.EndDateTime,
               x.EventId
           })
           .ToList();

        var registrations = new List<Registration>();
        foreach (var eventSlot in eventSlots)
        {
            var registration =
               Registration
                  .Create(dto.CareUserId,
                     eventSlot.EventSlotId, eventSlot.StartDateTime, eventSlot.EndDateTime,
                     dto.Status.Value);
            registrations.Add(registration);
        }

        DbSet.AddRange(registrations);
        await Context.SaveChangesAsync()
           .ConfigureAwait(false);

        return await Queryable
           .Where(x => x.EventSlot.EventId == dto.EventId && x.CareUserId == dto.CareUserId)
           .Select(Projector)
           .ToListAsync()
           .ConfigureAwait(false);
    }

    public async Task<SearchResults<EventSlotRegistrationsDTO>> GetAsync(
       Guid eventId,
       string filter,
       Expression<Func<EventSlotRegistrationsDTO, object>> orderer = null,
       ListSortDirection sortDirection = ListSortDirection.Ascending,
       int pageIndex = 0,
       int itemsPerPage = 15)
    {
        if (itemsPerPage < 1)
            throw new BadInputException("Invalid pageSize provided", ErrorMessages.PageSizeLessThanOne);

        orderer ??= x => x.Id;

        var eventSlotsWithRegistrations = await Context.EventSlots
           .Where(x => x.EventId == eventId)
           .Select(x => new EventSlotRegistrationsDTO
           {
               Id = x.Id,
               StartDateTime = x.StartDateTime,
               EndDateTime = x.EndDateTime,
               Registrations = x.Registrations.Select(reg => new EventCareUserRegistrationDTO()
               {
                   RegistrationId = reg.Id,
                   CareUserId = reg.CareUserId,
                   FirstName = reg.CareUser.User.FirstName,
                   LastName = reg.CareUser.User.LastName,
                   Status = reg.Status,
                   DaycareLocation = reg.DaycareLocationId != null
                  ? new DaycareLocationDTO()
                  {
                      Id = reg.DaycareLocationId.Value,
                      Name = reg.DaycareLocation.Name
                  }
                  : null
               }).ToList()
           })
           .OrderBy(orderer, sortDirection)
           .TakePage(pageIndex, itemsPerPage)
           .ToListAsync();

        var totalItemCount = await Queryable
           .CountAsync(x => x.EventSlot.EventId == eventId);


        return new SearchResults<EventSlotRegistrationsDTO>(eventSlotsWithRegistrations, totalItemCount, pageIndex);
    }

    public async Task<RegistrationDTO> GetOneBySlotAsync(Guid eventSlotId, Guid careUserId)
    {
        var registration = await Context.Registrations
           .Where(x => x.EventSlotId == eventSlotId && x.CareUserId == careUserId)
           .Select(Projector)
           .FirstOrDefaultAsync()
           .ConfigureAwait(false);

        return registration;
    }

    public async Task<RegistrationDTO> GetOneAsync(Guid eventId, Guid registrationId)
    {
        var registration = await Queryable
           .Where(x => x.Id == registrationId && x.EventSlot.EventId == eventId)
           .Select(Projector)
           .FirstOrDefaultAsync()
           .ConfigureAwait(false);

        if (registration == default)
            throw new NotFoundException($"There is no registration with id {registrationId} and event id {eventId}");

        return registration;
    }

    public Task<List<CsvRegistrationDTO>> GetParticipantsForSlotAsync(Guid eventId, Guid eventSlotId)
    {
        return Queryable
           .Where(x =>
              x.EventSlot.Event.Id == eventId &&
              x.EventSlotId == eventSlotId &&
              x.Status == RegistrationStatus.Accepted)
           .Select(x => new CsvRegistrationDTO
           {
               FirstName = x.CareUser.User.FirstName,
               LastName = x.CareUser.User.LastName,
               AgeGroup = x.CareUser.AgeGroup,
               DayCareAfterEndDateTime = x.EventSlot.Event.DayCareAfterEndDateTime,
               DayCareBeforeStartDateTime = x.EventSlot.Event.DayCareBeforeStartDateTime,
               Status = x.Status,
               IsExtern = x.CareUser.IsExtern,
           })
           .ToListAsync();
    }

    public async Task<RegistrationDTO> UpdateStatusAsync(Guid eventId, Guid registrationId, RegistrationStatus status)
    {
        var registration = await Context.Registrations
           .Where(x => x.Id == registrationId && x.EventSlot.EventId == eventId)
           .FirstOrDefaultAsync()
           .ConfigureAwait(false);

        if (registration == default)
            throw new NotFoundException($"There is no registration with id {registrationId} and event id {eventId}");

        Context.Entry(registration).State = EntityState.Detached;
        registration.Status = status;
        Context.Update(registration);
        await Context.SaveChangesAsync()
           .ConfigureAwait(false);

        return Projector.Compile()(registration);
    }

    public async Task DeleteAsync(Guid eventId, Guid registrationId)
    {
        var registration = await Context.Registrations
           .Where(x => x.Id == registrationId && x.EventSlot.EventId == eventId)
           .Select(Projector)
           .FirstOrDefaultAsync()
           .ConfigureAwait(false);

        if (registration == default)
            throw new NotFoundException($"There is no registration with id {registrationId} and event id {eventId}");
        Context.Remove(registration);
        await Context.SaveChangesAsync()
           .ConfigureAwait(false);
    }

    public async Task<UserRegisteredDTO> GetUserRegistrationStatus(Guid eventId, Guid careUserId)
    {
        var eventSlots = await Context.EventSlots
           .Include(x => x.Event)
           .Where(x => x.EventId == eventId)
           .Select(eventSlot => eventSlot.Event.RegistrationOnDailyBasis).ToListAsync();

        var registrations = await Context.Registrations
           .Where(x =>
              x.CareUserId == careUserId &&
              x.EventSlot.EventId == eventId)
           .Select(x => x.Status)
           .ToListAsync();

        var userIsRegisteredForAllEventSlots = eventSlots.Count() == registrations.Count();

        if (registrations.Any())
        {
            var pendingStatesRemaining = registrations.Count(x => x == RegistrationStatus.Pending);
            return new UserRegisteredDTO()
            {
                CareUserId = careUserId,
                IsRegisteredForAllEventslots = userIsRegisteredForAllEventSlots,
                PendingStatesRemaining = pendingStatesRemaining,
                Status = pendingStatesRemaining >= 0 ? RegistrationStatus.Pending : registrations.First()
            };
        }
        return new UserRegisteredDTO()
        {
            CareUserId = careUserId,
            PendingStatesRemaining = 0,
            IsRegisteredForAllEventslots = false
        };

    }

    public Task<List<RegistrationDTO>> GetAllSlotsForEventAsync(Guid eventId)
    {
        return Queryable
           .Where(x => x.EventSlot.EventId == eventId)
           .Select(Projector)
           .ToListAsync();
    }

    public async Task<RegistrationDTO> CreateOneBySlotAsync(CreateEventSlotRegistrationDTO dto)
    {
        if (dto == null)
            throw new BadInputException("Input to create registration cannot be null", ErrorMessages.NoDataPassed);

        var slot = await Context.EventSlots.FirstOrDefaultAsync(x => x.Id == dto.EventSlotId);
        if (slot == null)
            throw new NotFoundException("Event slot Id could not be found!", ErrorMessages.EventSlotNotFound);

        DbSet.Add(Registration.Create(dto.CareUserId, slot.Id, slot.StartDateTime, slot.EndDateTime,
           dto.Status.Value));

        await Context.SaveChangesAsync().ConfigureAwait(false);

        var registration = await Queryable
           .Where(x => x.EventSlotId == dto.EventSlotId && x.CareUserId == dto.CareUserId)
           .Select(Projector)
           .FirstOrDefaultAsync()
           .ConfigureAwait(false);

        return registration;
    }

    public async Task<RegistrationStatus> AcceptRegistration(Guid registrationId, Guid executedByUserId)
    {
        var registration = await Context.Registrations.SingleAsync(x => x.Id == registrationId);
        var originalStatus = registration.Status;
        registration.Status = RegistrationStatus.Accepted;

        await _actionNotificationService.RegisterEventRegistrationStatusChange(
           registrationId, executedByUserId, originalStatus, registration.Status);

        await Context.SaveChangesAsync();
        return registration.Status;
    }

    public async Task<RegistrationStatus> RejectRegistration(Guid registrationId, Guid executedByUserId)
    {
        var registration = await Context.Registrations.SingleAsync(x => x.Id == registrationId);
        var previousRegistrationStatus = registration.Status;
        registration.Status = RegistrationStatus.Rejected;

        await _actionNotificationService.RegisterEventRegistrationStatusChange(registrationId, executedByUserId,
           previousRegistrationStatus, registration.Status);

        await Context.SaveChangesAsync();

        return registration.Status;
    }

    public async Task<DaycareLocationDTO> UpdateDaycareLocationForRegistration(Guid registrationId,
       Guid locationId, Guid executedByUserId)
    {
        var location = await Context.SingerLocations.SingleAsync(x => x.Id == locationId);
        var registration = await Context.Registrations.SingleAsync(x => x.Id == registrationId);
        registration.DaycareLocationId = locationId;

        await _actionNotificationService.RegisterEventRegistrationLocationChange(registrationId, executedByUserId,
           location.Id, locationId);

        await Context.SaveChangesAsync();
        return new DaycareLocationDTO()
        {
            Name = location.Name,
            Id = location.Id
        };
    }

    public async Task<SearchResults<RegistrationDTO>> GetPendingRegistrations(
       Expression<Func<RegistrationDTO, object>> orderer = null,
       ListSortDirection sortDirection = ListSortDirection.Ascending,
       int pageSize = 15, int pageIndex = 0)
    {
        Expression<Func<Registration, bool>> filterExpression = f => f.Status == RegistrationStatus.Pending;

        var registrations = await Queryable.ToPagedListAsync<Registration, RegistrationDTO>(
           Mapper, filterExpression, orderer, sortDirection, pageIndex, pageSize
        );

        return registrations;
    }
}
