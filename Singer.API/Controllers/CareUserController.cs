using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singer.Data;
using Singer.DTOs;
using Singer.Services;
using Singer.Services.Interfaces;
using AutoMapper.QueryableExtensions;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   public class CareUserController : Controller
   {
      private IUserService _userService;
      private ApplicationDbContext _appContext;
      public CareUserController(IUserService service, ApplicationDbContext appContext, IMapper mapper)
      {
         _userService = service;
         _appContext = appContext;
      }

      [HttpGet]
      [ProducesResponseType(StatusCodes.Status200OK)]
      public async Task<ActionResult<PaginationDTO<CareUserDTO>>> GetUsers(
         string sortDirection = "asc",
         string sortColumn = "name",
         int pageIndex = 0,
         int pageSize = 15)
      {
         var sorter = new Sorter<CareUserDTO>(){sortColumn};
         var result = await _userService.GetUsersAsync<CareUserDTO>(pageIndex, pageSize, null, sorter);
         var requestPath = HttpContext.Request.Path;
         var nextPage = (pageIndex * pageSize) + result.Size >= result.TotalCount
            ? null
            : $"{requestPath}?PageIndex={pageIndex + pageSize}&Size={pageSize}";
         var page = new PaginationDTO<CareUserDTO>
         {
            Items = result.Items,
            Size = result.Items.Count,
            PageIndex = pageIndex,
            CurrentPageUrl = $"{requestPath}?{HttpContext.Request.QueryString.ToString()}",
            NextPageUrl = nextPage,
            PreviousPageUrl = pageIndex == 0
               ? null
               : $"{requestPath}?PageIndex={pageIndex - pageSize}&Size={pageSize}",
            TotalSize = result.TotalCount
         };
         return Ok(page);
      }

      [HttpGet("{id}")]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status200OK)]
      public async Task<ActionResult<CareUserDTO>> GetUser(Guid id)
      {
         var user = await _userService.GetUserAsync<CareUserDTO>(id);
         if (user == null)
         {
            return NotFound();
         }
         return Ok(user);
      }

      [HttpPost]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      public async Task<ActionResult<CareUserDTO>> CreateUser(CreateCareUserDTO user)
      {
         var returnUser = await _userService.CreateUserAsync<CreateCareUserDTO, CareUserDTO>(user);
         return Created(nameof(GetUser), returnUser);
      }

      [HttpPut("{id}")]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      public async Task<ActionResult> UpdateUser(string id, CareUserDTO user)
      {
         try
         {
            var result = await _userService.UpdateUserAsync<CareUserDTO>(user, Guid.Parse(id));
            if (result)
            {
               return NoContent();
            }
            else
            {
               return BadRequest();
            }
         }
         catch
         {
            return BadRequest();
         }
      }

      [HttpDelete("{id}")]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      public async Task<ActionResult> DeleteUser(string id)
      {
         try
         {
            await _userService.DeleteUserAsync(Guid.Parse(id));
         }
         catch
         {
            return BadRequest();
         }
         return NoContent();
      }
   }
}
