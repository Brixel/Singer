using Singer.DTOs;
using Singer.DTOs.Csv;
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
      Task<SearchResults<EventSlotRegistrationsDTO>> GetAsync(
         Guid eventId,
         string filter,
         Expression<Func<EventSlotRegistrationsDTO, object>> orderer = null,
         ListSortDirection sortDirection = ListSortDirection.Ascending,
         int pageIndex = 0,
         int itemsPerPage = 15);

      Task<List<EventRegistrationDTO>> GetAllSlotsForEventAsync(Guid eventId);

      Task<EventRegistrationDTO> GetOneBySlotAsync(Guid eventSlotId, Guid careUserId);
      Task<EventRegistrationDTO> GetOneAsync(Guid eventId, Guid registrationId);
      Task<List<CsvRegistrationDTO>> GetParticipantsForSlotAsync(Guid eventId, Guid eventSlotId);

      Task<IReadOnlyList<EventRegistrationDTO>> CreateAsync(CreateEventRegistrationDTO dto);
      Task<EventRegistrationDTO> CreateOneBySlotAsync(CreateEventSlotRegistrationDTO dto);

      Task<EventRegistrationDTO> UpdateStatusAsync(Guid eventId, Guid registrationId, RegistrationStatus status);

      Task DeleteAsync(Guid eventId, Guid registrationId);
      Task<UserRegisteredDTO> GetUserRegistrationStatus(Guid eventId, Guid careUserId);

      Task<RegistrationStatus> AcceptRegistration(Guid registrationId);
      Task<RegistrationStatus> RejectRegistration(Guid registrationId);
      Task<DaycareLocationDTO> UpdateDaycareLocationForRegistration(Guid registrationId, Guid locationId);
   }
}
