using Singer.Models;
using Singer.DTOs;
using Singer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   public class EventLocationController : DataControllerBase<EventLocation, EventLocationDTO, CreateEventLocationDTO>
   {
      public EventLocationController(EventLocationService databaseService) : base(databaseService)
      {
      }
   }
}
