using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.DTOs;
using Singer.Models;
using Singer.Services.Interfaces;

namespace Singer.Services
{
   public class EventService : DatabaseService<Event, EventDTO, CreateEventDTO>, IEventService
   {
      public EventService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
      {
      }

      protected override DbSet<Event> DbSet => Context.Events;

      protected override IQueryable<Event> Queryable => Context.Events
         .Include(x => x.Location)
         .Include(x => x.Registrations);

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
   }
}
