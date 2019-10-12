using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs;
using Singer.Models;
using Singer.Services;
using Singer.Services.Interfaces;

namespace Singer.Controllers
{
   [Authorize]
   public class EventController : DataControllerBase<Event, EventDTO, CreateEventDTO, UpdateEventDTO>
   {
      private readonly IEventService _eventService;
      private readonly ICareUserService _careUserService;

      public EventController(IEventService eventService, ICareUserService careUserService) : base(eventService)
      {
         _eventService = eventService;
         _careUserService = careUserService;
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

      [HttpGet("{id}/getcareusers")]
      public async Task<IActionResult> GetCareUsers(Guid id)
      {
         var careUsers = await _careUserService.GetCareUsersForLegalGuardian(Guid.Parse(User.GetSubjectId()));
         var singerEvent = await _eventService.GetOneAsync(id);
         foreach (var user in careUsers)
         {
            user.AppropriateAgeGroup = singerEvent.AllowedAgeGroups.Contains(user.AgeGroup);
         }
         return Ok(careUsers);
      }
   }
}
