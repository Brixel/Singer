using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs;
using Singer.Models;
using Singer.Services;
using Singer.Services.Interfaces;

namespace Singer.Controllers
{
   public class EventController : DataControllerBase<Event, EventDTO, CreateEventDTO, UpdateEventDTO>
   {
      private readonly IEventService _eventService;

      public EventController(IEventService eventService) : base(eventService)
      {
         _eventService = eventService;
      }
      [HttpPost("search")]
      public async Task<IActionResult> GetPublicEvents([FromBody] SearchEventParamsDTO searchEventParams)
      {
         var model = ModelState;
         if (model.IsValid)
         {
            var events = await _eventService.GetPublicEventsAsync(searchEventParams);
            return Ok(events);
         }

         return BadRequest(model);
      }
   }
}
