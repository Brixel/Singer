using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
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

      [HttpPut("sendemail")]
      public async Task SendEmail()
      {
         var subjectId = User.GetSubjectId();
         var userId = Guid.Parse(subjectId);
         await _actionNotificationService.SendEmails(userId);
      }
   }
}
