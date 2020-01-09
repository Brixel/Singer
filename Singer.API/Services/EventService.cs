using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.DTOs;
using Singer.Models;
using Singer.Services.Interfaces;
using Singer.Profiles;
using Singer.Helpers.Extensions;
using Singer.Helpers.Enums;
using Singer.Helpers.Exceptions;
using Singer.Resources;

namespace Singer.Services
{
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

      public async Task<IReadOnlyList<EventDescriptionDTO>> GetPublicEventsAsync(SearchEventParamsDTO searchEventParamsDto)
      {
         return await Queryable
            .Where(x =>
               // not archived
               !x.IsArchived &&
               // Only events today or in the future
               x.EventSlots.OrderBy(y => y.StartDateTime).First().StartDateTime.Date >= DateTime.Now.Date &&
               // check location
               (!searchEventParamsDto.LocationId.HasValue || x.LocationId == searchEventParamsDto.LocationId.Value) &&
               // check start date
               (!searchEventParamsDto.StartDate.HasValue ||
                  x.EventSlots.OrderBy(y => y.StartDateTime).First().StartDateTime.Date == searchEventParamsDto.StartDate.Value.Date) &&
               // check end date
               (!searchEventParamsDto.EndDate.HasValue ||
                  x.EventSlots.OrderBy(y => y.EndDateTime).First().EndDateTime.Date <= searchEventParamsDto.EndDate.Value.Date))
            .Select(x => new EventDescriptionDTO
            {
               Id = x.Id,
               AgeGroups = EventProfile.ToAgeGroupList(x.AllowedAgeGroups),
               Description = x.Description,
               Title = x.Title,
               StartDateTime = x.EventSlots.OrderBy(y => y.StartDateTime).First().StartDateTime,
               EndDateTime = x.EventSlots.OrderByDescending(y => y.EndDateTime).First().EndDateTime
            })
            .ToListAsync()
            .ConfigureAwait(false);
      }

      private IEnumerable<EventSlot> GenerateEventSlots(CreateEventDTO dto)
      {
         ValidateInput(dto);

         switch (dto.RepeatSettings?.RepeatType)
         {
            case RepeatType.OnDate:
               return EventSlot.GenerateEventSlotsUntilIncluding(
                  dto.StartDateTime,
                  dto.EndDateTime,
                  dto.RepeatSettings.StopRepeatDate,
                  dto.RepeatSettings.IntervalUnit);
            case RepeatType.AfterXTimes:
               return EventSlot.GenerateNumberOfEventSlots(
                  dto.StartDateTime,
                  dto.EndDateTime,
                  dto.RepeatSettings.NumberOfRepeats,
                  dto.RepeatSettings.IntervalUnit);
            default:
               return EventSlot.GenerateNumberOfEventSlots(
                  dto.StartDateTime,
                  dto.EndDateTime,
                  1,
                  default);
         }
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
}
