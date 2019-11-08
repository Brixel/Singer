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
using Singer.Helpers.Exceptions;
using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Profiles;
using Singer.Services.Interfaces;

namespace Singer.Services
{
   public class EventRegistrationService : IEventRegistrationService
   {
      public EventRegistrationService(ApplicationDbContext context, IMapper mapper)
      {
         Context = context;
         Mapper = mapper;
      }

      protected ApplicationDbContext Context { get; }
      protected IMapper Mapper { get; }

      protected DbSet<EventRegistration> DbSet =>
         Context.EventRegistrations;
      protected IQueryable<EventRegistration> Queryable => Context.EventRegistrations
         .Include(x => x.CareUser)
         .ThenInclude(x => x.User).AsQueryable();

      public Expression<Func<EventRegistration, EventRegistrationDTO>> Projector => x => new EventRegistrationDTO
      {
         Id = x.Id,
         Status = x.Status,
         CareUser = new CareUserDTO()
         {
            Id = x.CareUserId,
            AgeGroup = x.CareUser.AgeGroup,
            BirthDay = x.CareUser.BirthDay,
            FirstName = x.CareUser.User.FirstName,
            LastName = x.CareUser.User.LastName
         },
         EventDescription = new EventDescriptionDTO
         {
            Id = x.EventSlot.Event.Id,
            AgeGroups = EventProfile.ToAgeGroupList(x.EventSlot.Event.AllowedAgeGroups),
            Description = x.EventSlot.Event.Description,
            Title = x.EventSlot.Event.Title,
            StartDate = x.EventSlot.Event.EventSlots.OrderBy(y => y.StartDateTime).First().StartDateTime,
            EndDate = x.EventSlot.Event.EventSlots.OrderByDescending(y => y.EndDateTime).First().EndDateTime
         },
         EventSlot = new EventSlotDTO
         {
            StartDateTime = x.EventSlot.StartDateTime,
            EndDateTime = x.EventSlot.EndDateTime,
            Id = x.EventSlot.Id
         },
      };

      protected static Expression<Func<EventRegistration, bool>> Filter(string filter)
      {
         return o =>
            o.CareUser.User.FirstName.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
            o.CareUser.User.LastName.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
            o.EventSlot.Event.Location.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
            o.EventSlot.Event.Title.Contains(filter, StringComparison.InvariantCultureIgnoreCase);
      }

      public async Task<IReadOnlyList<EventRegistrationDTO>> CreateAsync(CreateEventRegistrationDTO dto)
      {
         if (dto == null)
            throw new BadInputException("Input to create registration cannot be null");

         var eventSlots = Context.EventSlots
            .Where(x => x.EventId == dto.EventId)
            .Select(x => new
            {
               EventSlotId = x.Id,
               x.EventId
            })
            .ToList();

         var registrations = new List<EventRegistration>();
         foreach (var eventSlot in eventSlots)
         {
            registrations.Add(new EventRegistration()
            {
               CareUserId = dto.CareUserId,
               EventSlotId = eventSlot.EventSlotId,
               Status = dto.Status.Value
            });
         }

         DbSet.AddRange(registrations);
         await Context.SaveChangesAsync()
            .ConfigureAwait(false);

         return await Queryable
            .Where(x => x.EventSlot.EventId == dto.EventId && x.CareUserId == dto.CareUserId)
            .Select(Projector)
            .ToListAsync()
            .ConfigureAwait(false);
      }

      public async Task<SearchResults<EventRegistrationDTO>> GetAsync(
         Guid eventId,
         string filter,
         Expression<Func<EventRegistrationDTO, object>> orderer = null,
         ListSortDirection sortDirection = ListSortDirection.Ascending,
         int pageIndex = 0,
         int itemsPerPage = 15)
      {
         if (itemsPerPage < 1)
            throw new BadInputException("Invalid pageSize provided");

         var filteredItems = Context.EventRegistrations
            .Include(x => x.CareUser)
            .ThenInclude(x => x.User)
            .Where(x => x.EventSlot.EventId == eventId);
         var totalItemCount = await filteredItems
            .CountAsync()
            .ConfigureAwait(false);

         var list = totalItemCount <= 0
            ? new List<EventRegistrationDTO>()
            : await filteredItems
               .Select(Projector)
               .OrderBy(orderer, sortDirection)
               .TakePage(pageIndex, itemsPerPage)
               .ToListAsync()
               .ConfigureAwait(false);

         return new SearchResults<EventRegistrationDTO>(list, totalItemCount, pageIndex);
      }

      public async Task<EventRegistrationDTO> GetOneBySlotAsync(Guid eventSlotId, Guid careUserId)
      {
         var registration = await Context.EventRegistrations
            .Where(x => x.EventSlotId == eventSlotId && x.CareUserId == careUserId)
            .Select(Projector)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

         return registration;
      }
      public async Task<EventRegistrationDTO> GetOneAsync(Guid eventId, Guid registrationId)
      {
         var registration = await Queryable
            .Where(x => x.Id == registrationId && x.EventSlot.EventId == eventId)
            .Select(Projector)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

         if (registration == default)
            throw new NotFoundException($"There is no registration with id {registrationId} and event id {eventId}");

         return registration;
      }

      public async Task<EventRegistrationDTO> UpdateStatusAsync(Guid eventId, Guid registrationId, RegistrationStatus status)
      {
         var registration = await Context.EventRegistrations
            .Where(x => x.Id == registrationId && x.EventSlot.EventId == eventId)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

         if (registration == default)
            throw new NotFoundException($"There is no registration with id {registrationId} and event id {eventId}");

         Context.Entry(registration).State = EntityState.Detached;
         registration.Status = status;
         Context.Update(registration);
         await Context.SaveChangesAsync()
            .ConfigureAwait(false);

         return Projector.Compile()(registration);
      }

      public async Task DeleteAsync(Guid eventId, Guid registrationId)
      {
         var registration = await Context.EventRegistrations
            .Where(x => x.Id == registrationId && x.EventSlot.EventId == eventId)
            .Select(Projector)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

         if (registration == default)
            throw new NotFoundException($"There is no registration with id {registrationId} and event id {eventId}");

         Context.Remove(registration);
         await Context.SaveChangesAsync()
            .ConfigureAwait(false);
      }

      public async Task<UserRegisteredDTO> GetUserRegistrationStatus(Guid eventId, Guid careUserId)
      {
         var eventSlots = await Context.EventSlots
            .Include(x => x.Event)
            .Where(x => x.EventId == eventId)
            .Select(eventSlot => eventSlot.Event.RegistrationOnDailyBasis).ToListAsync();

         var registrations = await Context.EventRegistrations
            .Where(x =>
               x.CareUserId == careUserId &&
               x.EventSlot.EventId == eventId)
            .Select(x => x.Status)
            .ToListAsync();

         var userIsRegisteredForAllEventSlots = eventSlots.Count() == registrations.Count();

         if (eventSlots.Any())
         {
            var pendingStatesRemaining = registrations.Count(x => x == RegistrationStatus.Pending);
            return new UserRegisteredDTO(){
               CareUserId = careUserId,
               IsRegistered = true,
               IsRegisteredForAllEventslots = userIsRegisteredForAllEventSlots,
               PendingStatesRemaining = pendingStatesRemaining,
               Status = pendingStatesRemaining > 0 ? RegistrationStatus.Pending : registrations.First()
            };
         }
         return new UserRegisteredDTO(){
            CareUserId = careUserId,
            IsRegistered = false,
            PendingStatesRemaining = 0,
            IsRegisteredForAllEventslots = false
         };

      }

      public Task<List<EventRegistrationDTO>> GetAllSlotsForEventAsync(Guid eventId)
      {
         return Queryable.Where(x => x.EventSlot.EventId == eventId).Select(Projector).ToListAsync();
      }

      public async Task<EventRegistrationDTO> CreateOneBySlotAsync(CreateEventSlotRegistrationDTO dto)
      {
         if (dto == null)
            throw new BadInputException("Input to create registration cannot be null");

         var slot = await Context.EventSlots.FirstOrDefaultAsync(x => x.Id == dto.EventSlotId);
         if (slot == null)
         {
            throw new BadInputException("Event slot Id could not be found!");
         }

         DbSet.Add(new EventRegistration()
         {
            CareUserId = dto.CareUserId,
            EventSlotId = dto.EventSlotId,
            Status = dto.Status.Value
         });
         await Context.SaveChangesAsync().ConfigureAwait(false);

         var registration = await Queryable
            .Where(x => x.EventSlotId == dto.EventSlotId && x.CareUserId == dto.CareUserId)
            .Select(Projector).FirstOrDefaultAsync().ConfigureAwait(false);

         return registration;
      }
   }
}
