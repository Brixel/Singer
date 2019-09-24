using System.Collections.Generic;
using System.Threading.Tasks;
using Singer.DTOs;
using Singer.Models;

namespace Singer.Services.Interfaces
{
   public interface IEventService : IDatabaseService<Event, EventDTO, CreateEventDTO, UpdateEventDTO>
   {
      Task<IReadOnlyList<EventDescriptionDTO>> GetPublicEventsAsync(SearchEventParamsDTO searchEventParams);
   }
}
