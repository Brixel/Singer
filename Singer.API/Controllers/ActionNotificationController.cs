using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs;
using Singer.Models;
using Singer.Services.Interfaces;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   [Authorize]
   public class ActionNotificationController : ControllerBase
   {
      private readonly IActionNotificationService _actionNotificationService;

      public ActionNotificationController(IActionNotificationService actionNotificationService)
      {
         _actionNotificationService = actionNotificationService;
      }

      [HttpGet("pending")]
      public async Task<List<EventRegistrationLogCareUserDTO>> GetEventRegistrationLogsWaitingForAction()
      {
         return await _actionNotificationService.GetEventRegistrationLogsWaitingForAction();
      }
   }
}
