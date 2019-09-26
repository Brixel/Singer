using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.DTOs;
using Singer.Models;
using Singer.Services.Interfaces;
using Singer.Profiles;

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

      public IReadOnlyList<EventDescriptionDTO> GetPublicEvents(SearchEventParamsDTO searchEventParamsDto)
      {
         var today = DateTime.Today;

         Expression<Func<EventSlot, bool>> useStartDate =
            x => x.StartDateTime >= today &&
                 (!searchEventParamsDto.StartDate.HasValue || x.StartDateTime == searchEventParamsDto.StartDate.Value) &&
                 (!searchEventParamsDto.EndDate.HasValue || x.EndDateTime <= searchEventParamsDto.EndDate.Value) &&
            (!searchEventParamsDto.LocationId.HasValue || x.Event.LocationId == searchEventParamsDto.LocationId.Value);

         var filteredEvents = Context.EventSlots.Where(useStartDate);
         return filteredEvents.Select(x => new EventDescriptionDTO()
         {
            AgeGroups = EventProfile.ToAgeGroupList(x.Event.AllowedAgeGroups),
            Description = x.Event.Description,
            Title = x.Event.Title,
            StartDate = x.StartDateTime,
            EndDate = x.EndDateTime
         }).ToList();
      }
   }
}
