using Singer.Models;
using Singer.DTOs;
using Singer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   [Authorize]
   public class RegistrationController : Controller
   {
      private IRegistrationService _registrationService;
      public RegistrationController(IRegistrationService registrationService)
      {
         _registrationService = registrationService;
      }

      [HttpPost("search")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<IActionResult> Search([FromBody] RegistrationSearchDTO searchDTO)
      {
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
   }
}
