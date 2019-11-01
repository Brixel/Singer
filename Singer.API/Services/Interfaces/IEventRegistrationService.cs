using Singer.DTOs;
using Singer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Singer.Services.Interfaces
{
   public interface IEventRegistrationService
   {
      Task<SearchResults<EventRegistrationDTO>> GetAsync(
         Guid eventId,
         string filter,
         Expression<Func<EventRegistrationDTO, object>> orderer = null,
         ListSortDirection sortDirection = ListSortDirection.Ascending,
         int pageIndex = 0,
         int itemsPerPage = 15);

      Task<List<EventRegistrationDTO>> GetAllSlotsForEventAsync(Guid eventId);

      Task<EventRegistrationDTO> GetOneBySlotAsync(Guid eventSlotId, Guid careUserId);
      Task<EventRegistrationDTO> GetOneAsync(Guid eventId, Guid registrationId);

      Task<IReadOnlyList<EventRegistrationDTO>> CreateAsync(CreateEventRegistrationDTO dto);

      Task<EventRegistrationDTO> UpdateStatusAsync(Guid eventId, Guid registrationId, RegistrationStatus status);

      Task DeleteAsync(Guid eventId, Guid registrationId);
   }
}
