using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.DTOs;
using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Services.Interfaces;

namespace Singer.Services
{
   public class EventRegistrationService : DatabaseService<EventRegistration, EventRegistrationDTO, CreateEventRegistrationDTO, UpdateEventRegistrationDTO>, IEventRegistrationService
   {
      public EventRegistrationService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
      {
      }

      protected override DbSet<EventRegistration> DbSet => Context.EventRegistrations;

      protected override IQueryable<EventRegistration> Queryable => Context.EventRegistrations;

      public async Task<SearchResults<EventRegistrationDTO>> GetAsync(
         Expression<Func<EventRegistration, bool>> filter = null,
         Expression<Func<EventRegistration, EventRegistrationDTO>> projector = null,
         Expression<Func<EventRegistrationDTO, object>> orderer = null,
         ListSortDirection sortDirection = ListSortDirection.Ascending,
         int pageIndex = 0,
         int itemsPerPage = 15)
      {
         // set the projector if it is null
         if (projector == null)
            projector = EntityToDTOProjector;

         // return the paged results
         return await Queryable
            .ToPagedListAsync(
               filterExpression: filter,
               projectionExpression: projector,
               orderByLambda: orderer,
               sortDirection: sortDirection,
               pageIndex: pageIndex,
               pageSize: itemsPerPage);
      }

      protected override Expression<Func<EventRegistration, bool>> Filter(string filter)
      {
         return o => true;
      }
   }
}
