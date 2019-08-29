using Singer.DTOs;
using Singer.Models;
using Singer.Services.Interfaces;

namespace Singer.Controllers
{
   public class EventController : DataControllerBase<Event, EventDTO, CreateEventDTO, UpdateEventDTO>
   {
      public EventController(IEventService eventService) : base(eventService)
      {
      }
   }
}
