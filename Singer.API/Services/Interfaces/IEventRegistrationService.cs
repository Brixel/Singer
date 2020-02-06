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

      Task<List<RegistrationDTO>> GetAllSlotsForEventAsync(Guid eventId);

      Task<RegistrationDTO> GetOneBySlotAsync(Guid eventSlotId, Guid careUserId);
      Task<RegistrationDTO> GetOneAsync(Guid eventId, Guid registrationId);
      Task<List<CsvRegistrationDTO>> GetParticipantsForSlotAsync(Guid eventId, Guid eventSlotId);

      Task<IReadOnlyList<RegistrationDTO>> CreateAsync(CreateRegistrationDTO dto);
      Task<RegistrationDTO> CreateOneBySlotAsync(CreateEventSlotRegistrationDTO dto);

      Task<RegistrationDTO> UpdateStatusAsync(Guid eventId, Guid registrationId, RegistrationStatus status);

      Task DeleteAsync(Guid eventId, Guid registrationId);
      Task<UserRegisteredDTO> GetUserRegistrationStatus(Guid eventId, Guid careUserId);

      Task<RegistrationStatus> AcceptRegistration(Guid registrationId, Guid executedByUserId);
      Task<RegistrationStatus> RejectRegistration(Guid registrationId, Guid executedByUserId);
      Task<DaycareLocationDTO> UpdateDaycareLocationForRegistration(Guid registrationId, Guid locationId, Guid executedByUserId);
      Task<SearchResults<RegistrationDTO>> GetPendingRegistrations(Expression<Func<RegistrationDTO, object>> orderer = null, ListSortDirection sortDirection = ListSortDirection.Ascending, int pageSize = 15, int pageIndex = 0);
   }
}
