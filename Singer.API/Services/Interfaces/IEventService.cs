using System.Collections.Generic;
using Singer.DTOs;
using Singer.Models;

namespace Singer.Services.Interfaces
{
   public interface IEventService : IDatabaseService<Event, EventDTO, CreateEventDTO, UpdateEventDTO>
   {
      IReadOnlyList<EventDescriptionDTO> GetPublicEvents(SearchEventParamsDTO searchEventParams);
   }
}
