using Microsoft.AspNetCore.Authorization;
using Singer.Models;
using Singer.DTOs;
using Microsoft.AspNetCore.Mvc;
using Singer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   [Authorize]
   public class SingerLocationController : DataControllerBase<SingerLocation, SingerLocationDTO, CreateSingerLocationDTO, UpdateSingerLocationDTO>
   {
      private ISingerLocationService _singerLocationService;
      public SingerLocationController(ISingerLocationService eventLocationService) : base(eventLocationService)
      {
         _singerLocationService = eventLocationService;
      }

      [HttpPost("search")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<IActionResult> Search([FromBody] SingerLocationSearchDTO searchDTO)
      {
         var model = ModelState;
         if (!model.IsValid)
            return BadRequest(model);


         var result = await _singerLocationService.AdvancedSearch(searchDTO);
         var requestPath = HttpContext.Request.Path;
         var nextPage = (searchDTO.PageIndex * searchDTO.PageSize) + result.Size >= result.TotalCount
            ? null
            : $"{requestPath}?PageIndex={searchDTO.PageIndex++}&Size={searchDTO.PageSize}";

         var page = new PaginationDTO<SingerLocationDTO>
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
