using Singer.DTOs;
using Singer.Models;
using Singer.Services;
using Singer.Services.Interfaces;

namespace Singer.Controllers
{
   public class EventController : DataControllerBase<Event, EventDTO, CreateEventDTO>
   {
      public EventController(EventService databaseService) : base(databaseService)
      {
         
      }
      public void GetPublicEvents()
      {

      }
   }
}
