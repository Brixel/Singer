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

namespace Singer.Services
{
   public class EventService
      : DatabaseService<Event, EventDTO, CreateEventDTO, UpdateEventDTO>, IEventService
   {
      public EventService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
      {
      }


      protected override DbSet<Event> DbSet => Context.Events;

      protected override IQueryable<Event> Queryable => Context.Events.Include(x => x.Location);


      public override async Task<EventDTO> CreateAsync(CreateEventDTO dto)
      {
         var start = dto.StartDate;
         var end = dto.EndDate;

         // add slots for all the days in the event
         var slots = new List<EventSlot>();
         var diff = end - start;
         for (var i = 0; i <= diff.Days; i++)
         {
            var date = start + TimeSpan.FromDays(i);
            slots.Add(new EventSlot
            {
               StartDateTime = date,
               EndDateTime = date.SetTime(end),
            });
         }

         // project the DTO to an entity
         var entity = Mapper.Map<Event>(dto);
         entity.EventSlots = slots;

         Context.Add(entity);
         await Context.SaveChangesAsync();

         var returnEntity = await GetOneAsync(entity.Id);

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
               (!searchEventParamsDto.StartDate.HasValue || x.EventSlots.OrderBy(y => y.StartDateTime).First().StartDateTime == searchEventParamsDto.StartDate) &&
               // check end date
               (!searchEventParamsDto.EndDate.HasValue || x.EventSlots.OrderBy(y => y.EndDateTime).First().EndDateTime <= searchEventParamsDto.EndDate))
            .Select(x => new EventDescriptionDTO
            {
               AgeGroups = EventProfile.ToAgeGroupList(x.AllowedAgeGroups),
               Description = x.Description,
               Title = x.Title,
               StartDate = x.EventSlots.OrderBy(y => y.StartDateTime).First().StartDateTime,
               EndDate = x.EventSlots.OrderByDescending(y => y.EndDateTime).First().EndDateTime
            }).ToListAsync();
      }
   }
}
