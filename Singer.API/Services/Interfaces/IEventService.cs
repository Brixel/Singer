using Singer.DTOs;
using Singer.Models;
using System.Threading.Tasks;

namespace Singer.Services.Interfaces
{
   public interface IEventService : IDatabaseService<Event, EventDTO, CreateEventDTO>
   {
   }
}
