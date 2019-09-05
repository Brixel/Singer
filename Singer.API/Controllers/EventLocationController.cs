using Singer.Models;
using Singer.DTOs;
using Singer.Services;
using Microsoft.AspNetCore.Mvc;
using Singer.Services.Interfaces;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   public class EventLocationController : DataControllerBase<EventLocation, EventLocationDTO, CreateEventLocationDTO, UpdateEventLocationDTO>
   {
      public EventLocationController(IEventLocationService eventLocationService) : base(eventLocationService)
      {
      }
   }
}
