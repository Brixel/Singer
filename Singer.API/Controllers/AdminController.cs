using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs;
using Singer.Models;
using Singer.Models.Constants;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   [Authorize()]
   public class AdminController : Controller
   {
      [HttpGetAttribute]
      [ProducesResponseType(StatusCodes.Status200OK)]
      public async Task<ActionResult<PaginationDTO<AdminDTO>>> GetUsers(string sortDirection, string sortColumn, int pageIndex, int pageSize, string filter)
      {
         pageIndex++;
         //var result = await _userService.GetUsersAsync<CareUser>(sortColumn, sortDirection, filter, pageIndex, pageSize);
         var requestPath = HttpContext.Request.Path;
         var items = new List<AdminDTO>
         {
            new AdminDTO()
            {
               Email = "batman@singer.be",
               FirstName = "Bat",
               LastName = "Man",
               UserName = "bat.man",
               Id = Guid.NewGuid()
            },
            new AdminDTO()
            {
               Email = "superman@singer.be",
               FirstName = "Super",
               LastName = "Man",
               UserName = "super.man",
               Id = Guid.NewGuid()
            },
            new AdminDTO()
            {
               Email = "spider@singer.be",
               FirstName = "Spider",
               LastName = "Man",
               UserName = "spider.man",
               Id = Guid.NewGuid()
            },
            new AdminDTO()
            {
               Email = "woman@singer.be",
               FirstName = "Wo",
               LastName = "Man",
               UserName = "wo.man",
               Id = Guid.NewGuid()
            }
         };


         var page = new PaginationDTO<AdminDTO>
         {
            Items = items,
            Size = items.Count,
            PageIndex = pageIndex,
            CurrentPageUrl = $"{requestPath}?{HttpContext.Request.QueryString.ToString()}",
            NextPageUrl = "idc",
            PreviousPageUrl = pageIndex == 0
               ? null
               : $"{requestPath}?PageIndex={pageIndex - pageSize}&Size={pageSize}",
            TotalSize = 1337
         };
         return Ok(page);
      }
   }
}
