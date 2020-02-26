using Singer.DTOs;
using Singer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;
using Singer.Configuration;
using IdentityServer4.Extensions;
using System.Linq;
using Singer.Helpers.Extensions;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   [Authorize]
   public class RegistrationController : Controller
   {
      private IRegistrationService _registrationService;
      private ICareUserService _careUserService;
      public RegistrationController(IRegistrationService registrationService, ICareUserService careUserService)
      {
         _registrationService = registrationService;
         _careUserService = careUserService;
      }

      [HttpPost("search")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<IActionResult> Search([FromBody] RegistrationSearchDTO searchDTO)
      {
         var model = ModelState;
         if (!model.IsValid)
            return BadRequest(model);

         if (!User.IsInRole(Roles.ROLE_ADMINISTRATOR))
         {
            var careUserDTOs = await this._careUserService.GetCareUsersForLegalGuardianAsync(Guid.Parse(User.GetSubjectId()));
            if (careUserDTOs.Count == 0)
               return NotFound(model);
            searchDTO.CareUserIds = careUserDTOs.Select(x => x.UserId).ToList();
         }
         var result = await _registrationService.AdvancedSearch(searchDTO);
         var requestPath = HttpContext.Request.Path;
         var nextPage = (searchDTO.PageIndex * searchDTO.PageSize) + result.Size >= result.TotalCount
            ? null
            : $"{requestPath}?PageIndex={searchDTO.PageIndex++}&Size={searchDTO.PageSize}";

         var page = new PaginationDTO<RegistrationOverviewDTO>
         {
            Items = result.Items,
            Size = result.Items.Count,
            PageIndex = searchDTO.PageIndex,
            CurrentPageUrl = $"{requestPath}?{HttpContext.Request.QueryString.ToString()}",
            NextPageUrl = nextPage,
            PreviousPageUrl = searchDTO.PageIndex == 0
            ? null
            : $"{requestPath}?PageIndex={searchDTO.PageIndex--}&Size={searchDTO.PageSize}",
            TotalSize = result.TotalCount
         };

         return Ok(page);
      }

      [HttpPut("{registrationId}/accept")]
      [Authorize(Roles = Roles.ROLE_ADMINISTRATOR)]
      public async Task<ActionResult> AcceptRegistration(Guid registrationId)
      {
         var userId = User.GetUserId();
         var status = await _registrationService.AcceptRegistration(registrationId, userId);
         return Ok(status);
      }

      [HttpPut("{registrationId}/reject")]
      [Authorize(Roles = Roles.ROLE_ADMINISTRATOR)]
      public async Task<ActionResult> RejectRegistration(Guid registrationId)
      {
         var userId = User.GetUserId();
         var status = await _registrationService.RejectRegistration(registrationId, userId);
         return Ok(status);
      }
   }
}
