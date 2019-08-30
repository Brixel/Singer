using Singer.DTOs;
using Singer.Models;

namespace Singer.Services.Interfaces
{
   public interface IEventLocationService : IDatabaseService<EventLocation, EventLocationDTO, CreateEventLocationDTO, UpdateEventLocationDTO>
   { }
}
