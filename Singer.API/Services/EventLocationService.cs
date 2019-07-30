using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.DTOs;
using Singer.Models;

namespace Singer.Services
{
   public class EventLocationService : DatabaseService<EventLocation, EventLocationDTO, CreateEventLocationDTO>
   {
      public EventLocationService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
      {
      }

      protected override DbSet<EventLocation> DbSet => Context.EventLocations;

      protected override IQueryable<EventLocation> Queryable => Context.EventLocations;

      protected override Expression<Func<EventLocation, bool>> Filter(string filter)
      {
         if (string.IsNullOrWhiteSpace(filter))
         {
            return o => true;
         }
         Expression<Func<EventLocation, bool>> filterExpression =
            f =>
               f.Address.Contains(filter) ||
               f.City.Contains(filter) ||
               f.Country.Contains(filter) ||
               f.Name.Contains(filter) ||
               f.PostalCode.Contains(filter);
         return filterExpression;
      }
   }
}
