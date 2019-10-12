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
         var today = DateTime.Today;

         Expression<Func<Event, bool>> useStartDate =
            x => x.StartDateTime >= today &&
                 (!searchEventParamsDto.StartDate.HasValue || x.StartDateTime == searchEventParamsDto.StartDate.Value) &&
                 (!searchEventParamsDto.EndDate.HasValue || x.EndDateTime <= searchEventParamsDto.EndDate.Value) &&
            (!searchEventParamsDto.LocationId.HasValue || x.LocationId == searchEventParamsDto.LocationId.Value);

         var filteredEvents = Queryable.Where(useStartDate);
         return await filteredEvents.Select(x => new EventDescriptionDTO()
         {
            AgeGroups = EventProfile.ToAgeGroupList(x.AllowedAgeGroups),
            Description = x.Description,
            Title = x.Title,
            StartDate = x.StartDateTime,
            EndDate = x.EndDateTime
         }).ToListAsync();
      }
   }

   public class EventDescriptionDTO
   {
      public string Title { get; set; }
      public string Description { get; set; }
      public DateTime StartDate { get; set; }
      public DateTime EndDate { get; set; }
      public IReadOnlyList<AgeGroup> AgeGroups { get; set; }
   }

   public class SearchEventParamsDTO
   {
      public DateTime? StartDate { get; set; }
      public DateTime? EndDate { get; set; }
      public Guid? LocationId { get; set; }
   }
}
