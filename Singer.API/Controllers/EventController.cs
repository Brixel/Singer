using System.Collections.Generic;
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
      public IReadOnlyList<EventDescriptionDTO> GetPublicEvents([FromBody] SearchEventParamsDTO searchEventParams)
      {
         return _eventService.GetPublicEvents(searchEventParams);
      }
   }
}
