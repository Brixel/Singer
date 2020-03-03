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
   public class EventLocationService : DatabaseService<SingerLocation, SingerLocationDTO, CreateSingerLocationDTO, UpdateSingerLocationDTO>,
      IEventLocationService
   {
      public EventLocationService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
      {
      }

      protected override DbSet<SingerLocation> DbSet => Context.SingerLocations;

      protected override IQueryable<SingerLocation> Queryable => Context.SingerLocations;

      protected override Expression<Func<SingerLocation, bool>> Filter(string filter)
      {
         if (string.IsNullOrWhiteSpace(filter))
         {
            return o => true;
         }
         Expression<Func<SingerLocation, bool>> filterExpression =
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
