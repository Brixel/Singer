using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Singer.Data;
using Singer.DTOs;
using Singer.Helpers.Enums;
using Singer.Helpers.Exceptions;
using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Profiles;
using Singer.Resources;
using Singer.Services.Interfaces;

namespace Singer.Services;

public class EventService
  : DatabaseService<Event, EventDTO, CreateEventDTO, UpdateEventDTO>, IEventService
{
    public EventService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }


    protected override DbSet<Event> DbSet => Context.Events;

    protected override IQueryable<Event> Queryable => Context.Events
       .Include(x => x.Location)
       .Include(x => x.EventSlots);


    public override async Task<EventDTO> UpdateAsync(Guid id, UpdateEventDTO newValue)
    {
        if (newValue == null)
            throw new BadInputException("The value to create cannot be null", ErrorMessages.NoDataPassed);

        var singerEvent = await Context.Events
            .Include(x => x.EventSlots)
            .SingleOrDefaultAsync(x => x.Id == id);
        if (singerEvent == null)
        {
            throw new BadInputException($"No event found with id {id}", ErrorMessages.NotFoundError);
        }
        // add slots for all the days in the event
        var updatedEventSlots = UpdateEventSlots(newValue);
        Context.RemoveRange(singerEvent.EventSlots);
        singerEvent.EventSlots = updatedEventSlots.ToList();
        Context.Update(singerEvent);
        await Context.SaveChangesAsync();

        var returnEntity = await GetOneAsync(singerEvent.Id).ConfigureAwait(false);

        // return the new updated entity
        return returnEntity;
    }

    private IEnumerable<EventSlot> UpdateEventSlots(UpdateEventDTO newValue)
    {
        // Updating event slots doesn't take repeatsettings into account, so only "GenerateEventSlotsUntilIncluding" is used
        // This is the only supported implementation in the frontend right now, so it's not really an issues
        var eventSlots =
            EventSlot.GenerateEventSlotsUntilIncluding(
                newValue.StartDateTime, 
                newValue.EndDateTime, 
                newValue.EndDateTime,  // Hardcoded values since the other values aren't used in the frontend either
                TimeUnit.Day);  // Hardcoded values since the other values aren't used in the frontend either
        return eventSlots;
    }

    public override async Task<EventDTO> CreateAsync(CreateEventDTO dto)
    {
        if (dto == null)
            throw new BadInputException("The value to create cannot be null", ErrorMessages.NoDataPassed);

        // add slots for all the days in the event
        var slots = GenerateEventSlots(dto).ToList();

        // project the DTO to an entity
        var entity = Mapper.Map<Event>(dto);
        entity.EventSlots = slots;

        Context.Add(entity);
        await Context.SaveChangesAsync().ConfigureAwait(false);

        var returnEntity = await GetOneAsync(entity.Id).ConfigureAwait(false);

        // return the new created entity
        return returnEntity;
    }

    protected override Expression<Func<Event, bool>> Filter(string filter)
    {
        if (string.IsNullOrWhiteSpace(filter))
            return o => true;

        Expression<Func<Event, bool>> filterExpression =
           f =>
              f.Location.Name.Contains(filter) ||
              f.Title.Contains(filter) ||
              f.Description.Contains(filter);
        return filterExpression;
    }

    public async Task<IReadOnlyList<EventDescriptionDTO>> GetPublicEventsAsync(EventFilterParametersDTO eventFilterParametersDtoDto)
    {
        return await Queryable
           .Where(x =>
              // not archived
              !x.IsArchived &&
              // Only events today or in the future
              x.EventSlots.OrderBy(y => y.StartDateTime).First().StartDateTime.Date >= DateTime.Now.Date &&
              // check start date
              (!eventFilterParametersDtoDto.StartDate.HasValue ||
                 x.EventSlots.OrderBy(y => y.StartDateTime).First().StartDateTime.Date >= eventFilterParametersDtoDto.StartDate.Value.Date) &&
              // check end date
              (!eventFilterParametersDtoDto.EndDate.HasValue ||
                 x.EventSlots.OrderBy(y => y.EndDateTime).First().EndDateTime.Date <= eventFilterParametersDtoDto.EndDate.Value.Date) &&
              // check location
              (!eventFilterParametersDtoDto.LocationId.HasValue || x.LocationId == eventFilterParametersDtoDto.LocationId.Value) &&
              // check allowed agegroups
              (eventFilterParametersDtoDto.AllowedAgeGroups == null ||
              eventFilterParametersDtoDto.AllowedAgeGroups.Count == 0 ||
              eventFilterParametersDtoDto.AllowedAgeGroups.Any(ageGroup => x.AllowedAgeGroups.HasFlag(ageGroup))) &&
              // check event title
              (string.IsNullOrEmpty(eventFilterParametersDtoDto.Text) || x.Title.ToLower().Contains(eventFilterParametersDtoDto.Text.ToLower())))
           .Select(x => new EventDescriptionDTO
           {
               Id = x.Id,
               Title = x.Title,
               Description = x.Description,
               AgeGroups = EventProfile.ToAgeGroupList(x.AllowedAgeGroups),
               Cost = x.Cost,
               StartDateTime = x.EventSlots.OrderBy(y => y.StartDateTime).First().StartDateTime,
               EndDateTime = x.EventSlots.OrderByDescending(y => y.EndDateTime).First().EndDateTime
           })
           .ToListAsync()
           .ConfigureAwait(false);
    }

    private IEnumerable<EventSlot> GenerateEventSlots(CreateEventDTO dto)
    {
        ValidateInput(dto);

        return (dto.RepeatSettings?.RepeatType) switch
        {
            RepeatType.OnDate => EventSlot.GenerateEventSlotsUntilIncluding(
                               dto.StartDateTime,
                               dto.EndDateTime,
                               dto.RepeatSettings.StopRepeatDate,
                               dto.RepeatSettings.IntervalUnit),
            RepeatType.AfterXTimes => EventSlot.GenerateNumberOfEventSlots(
                               dto.StartDateTime,
                               dto.EndDateTime,
                               dto.RepeatSettings.NumberOfRepeats,
                               dto.RepeatSettings.IntervalUnit),
            _ => EventSlot.GenerateNumberOfEventSlots(
                               dto.StartDateTime,
                               dto.EndDateTime,
                               1,
                               default),
        };
    }

    private void ValidateInput(CreateEventDTO createEvent)
    {
        // Currently only validate this for event slot creation
        if (createEvent.StartDateTime > createEvent.EndDateTime)
        {
            throw new BadInputException("Start date cannot be after end date", ErrorMessages.BadInputError);
        }

        // Not required for event slot creation, yet it's a logical validation
        if (createEvent.StartRegistrationDateTime > createEvent.EndRegistrationDateTime)
        {
            throw new BadInputException("Start of registration cannot be after end of registration", ErrorMessages.BadInputError);
        }

        // Not required for event slot creation, yet it's a logical validation
        if (createEvent.StartRegistrationDateTime > createEvent.FinalCancellationDateTime)
        {
            throw new BadInputException("Start of registration cannot be after cancellation date", ErrorMessages.BadInputError);
        }

    }
}
