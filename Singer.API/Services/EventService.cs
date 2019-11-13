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
            throw new Helpers.Exceptions.BadInputException("The value to create cannot be null");

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
         {
            return o => true;
         }
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
         switch (dto.RepeatSettings?.RepeatType)
         {
            case RepeatType.OnDate:
               return EventSlot.GenerateEventSlotsUntil(
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
   }
}
