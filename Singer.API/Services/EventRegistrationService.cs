using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.DTOs;
using Singer.DTOs.Users;
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
         // return the paged results
         return await Queryable
            .ToPagedListAsync(
               filterExpression: filter,
               mapper: Mapper,
               orderByLambda: orderer,
               sortDirection: sortDirection,
               pageIndex: pageIndex,
               pageSize: itemsPerPage);
      }

      protected override Expression<Func<EventRegistration, bool>> Filter(string filter)
      {
         return o => true;
      }

      public override async Task<EventRegistrationDTO> CreateAsync(CreateEventRegistrationDTO dto)
      {
         var careUser = Context.CareUsers.Single(x => x.Id == dto.CareUserId);
         var eventSlots = Context.EventSlots.Select(x => new
            {
               EventSlotId = x.Id,
               x.EventId
            }).Where(x => x.EventId == dto.EventId).ToList();
         var registrations = new List<EventRegistration>();
         foreach (var eventSlot in eventSlots)
         {
            registrations.Add(new EventRegistration()
            {
               CareUserId = dto.CareUserId,
               EventSlotId = eventSlot.EventSlotId
            });
         }
         DbSet.AddRange(registrations);
         await Context.SaveChangesAsync();
         return new EventRegistrationDTO()
         {
            CareUser = Mapper.Map<CareUserDTO>(careUser),
            EventId = eventSlots.First().EventId,
            EventSlots = registrations.Select(x => new EventRegistrationDTO.EventSlotRegistrationDTO()
            {
               EventSlotId = x.EventSlotId,
               Id = x.Id,
               Status = x.Status
            }).ToList()
         };
      }
   }
}
