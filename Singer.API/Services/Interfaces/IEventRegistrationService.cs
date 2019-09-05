using Singer.DTOs;
using Singer.Models;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Singer.Services.Interfaces
{
   public interface IEventRegistrationService : IDatabaseService<EventRegistration, EventRegistrationDTO, CreateEventRegistrationDTO, UpdateEventRegistrationDTO>
   {
      Task<SearchResults<EventRegistrationDTO>> GetAsync(
        Expression<Func<EventRegistration, bool>> filter = null,
        Expression<Func<EventRegistration, EventRegistrationDTO>> projector = null,
        Expression<Func<EventRegistrationDTO, object>> orderer = null,
        ListSortDirection sortDirection = ListSortDirection.Ascending,
        int pageIndex = 0,
        int itemsPerPage = 15);
   }
}
