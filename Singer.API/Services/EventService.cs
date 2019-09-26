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
using Microsoft.EntityFrameworkCore.Internal;

namespace Singer.Services
{
   public class EventService : DatabaseService<Event, EventDTO, CreateEventDTO, UpdateEventDTO>,
      IEventService
   {
      public EventService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
      {
      }

      protected override DbSet<Event> DbSet => Context.Events;

      protected override IQueryable<Event> Queryable => Context.Events.Include(x => x.Location);

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
