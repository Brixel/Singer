using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs;
using Singer.Models;
using Singer.Services;
using Singer.Services.Interfaces;

namespace Singer.Controllers
{
   public class EventController : DataControllerBase<Event, EventDTO, CreateEventDTO>
   {
      private readonly EventService _databaseService;

      public EventController(EventService databaseService) : base(databaseService)
      {
         _databaseService = databaseService;
      }
      [HttpPost("search")]
      public IReadOnlyList<EventDescriptionDTO> GetPublicEvents([FromBody] SearchEventParamsDTO searchEventParams)
      {
         return _databaseService.GetPublicEvents(searchEventParams);
      }
   }
}
