using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.DTOs;
using Singer.Models;

namespace Singer.Services
{
   public class EventService : DatabaseService<Event, EventDTO, CreateEventDTO>
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
         return Queryable.Where(x => x.StartDate >= today).Select(x => new EventDescriptionDTO()
         {
            AgeGroups = x.AllowedAgeGroups,
            Description = x.Description,
            Title = x.Title

         }).ToList();
      }
   }

   public class EventDescriptionDTO
   {
      public string Title { get; set; }
      public string Description { get; set; }
      public AgeGroup AgeGroups { get; set; }
   }

   public class SearchEventParamsDTO
   {
      public DateTime? StartDate { get; set; }
      public DateTime? EndDate { get; set; }
      public Guid? LocationId { get; set; }
   }
}
