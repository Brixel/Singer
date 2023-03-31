using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Duende.IdentityServer.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Singer.Data;
using Singer.DTOs;
using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Services.Interfaces;

namespace Singer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActionNotificationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IActionNotificationService _actionNotificationService;

        public ActionNotificationController(ApplicationDbContext context, IActionNotificationService actionNotificationService)
        {
            _context = context;
            _actionNotificationService = actionNotificationService;
        }

        [HttpGet("pending")]
        public async Task<IReadOnlyList<EventRegistrationLogCareUserDTO>> GetEventRegistrationLogsWaitingForAction()
        {
            var pendingRegistrations =
               await _actionNotificationService.GetEventRegistrationLogsWaitingForAction();
            return pendingRegistrations.Select(x => x.CareUserLogDTO).ToList();
        }

        [HttpPut("sendemail")]
        public async Task SendEmail()
        {
            var userId = User.GetUserId();
            await _actionNotificationService.SendEmails(userId);
            await _context.SaveChangesAsync();
        }
    }
}
