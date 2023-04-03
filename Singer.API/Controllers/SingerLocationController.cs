using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Singer.DTOs;
using Singer.Models;
using Singer.Services.Interfaces;

namespace Singer.Controllers;

[Route("api/location")]
[Authorize]
public class SingerLocationController : DataControllerBase<SingerLocation, SingerLocationDTO, CreateSingerLocationDTO, UpdateSingerLocationDTO>
{
    private readonly ISingerLocationService _singerLocationService;
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
            CurrentPageUrl = $"{requestPath}?{HttpContext.Request.QueryString}",
            NextPageUrl = nextPage,
            PreviousPageUrl = searchDTO.PageIndex == 0
           ? null
           : $"{requestPath}?PageIndex={searchDTO.PageIndex--}&Size={searchDTO.PageSize}",
            TotalSize = result.TotalCount
        };

        return Ok(page);
    }

}
