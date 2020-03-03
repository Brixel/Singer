using Microsoft.AspNetCore.Authorization;
using Singer.Models;
using Singer.DTOs;
using Singer.Services;
using Microsoft.AspNetCore.Mvc;
using Singer.Services.Interfaces;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   [Authorize]
   public class EventLocationController : DataControllerBase<SingerLocation, SingerLocationDTO, CreateSingerLocationDTO, UpdateSingerLocationDTO>
   {
      public EventLocationController(IEventLocationService eventLocationService) : base(eventLocationService)
      {
      }
   }
}
