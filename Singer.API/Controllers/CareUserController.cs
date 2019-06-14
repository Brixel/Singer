using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singer.Data;
using Singer.DTOs;
using Singer.Services.Interfaces;
using Singer.Models;
using Singer.Services;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   public class CareUserController : Controller
   {
      private IUserService _userService;
      private readonly IMapper _mapper;

      public CareUserController(IUserService service, ApplicationDbContext appContext, IMapper mapper)
      {
         _userService = service;
         _mapper = mapper;
      }

      [HttpGet]
      [ProducesResponseType(StatusCodes.Status200OK)]
      public async Task<ActionResult<PaginationDTO<CareUserDTO>>> GetUsers(
         [FromQuery]int pageIndex = 0,
         [FromQuery]int pageSize = 15,
         [FromQuery]string filter = "",
         [FromQuery]string sortBy = "")
      {
         Sorter<CareUser> sort = null;
         if (sortBy.Length > 0)
         {
            sort = new Sorter<CareUser>();
            var sortColumns = sortBy.Split(",");
            foreach (var column in sortColumns)
            {
               sort.Add(column);
            }
         }

         StringFilter<CareUser> stringFilter = null;
         if (filter.Length > 0)
         {
            stringFilter = new StringFilter<CareUser> {FilterString = filter};
         }
         var result = await _userService.GetUsersAsync<CareUser>(pageIndex, pageSize, stringFilter, sort);
         var requestPath = HttpContext.Request.Path;
         var nextPage = (pageIndex * pageSize) + result.Size >= result.TotalCount
            ? null
            : $"{requestPath}?PageIndex={pageIndex + pageSize}&Size={pageSize}";

         var careUserDTOs = result.Items
            .AsQueryable()
            .ProjectTo<CareUserDTO>(_mapper.ConfigurationProvider)
            .ToList();

         var page = new PaginationDTO<CareUserDTO>
         {
            Items = careUserDTOs,
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
         var user = await _userService.GetUserAsync<CareUser>(id);
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
         var model = _mapper.Map<CareUser>(user);
         var returnUser = await _userService.CreateUserAsync(model);
         return Created(nameof(GetUser), returnUser);
      }

      [HttpPut("{id}")]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      public async Task<ActionResult> UpdateUser(string id, CareUserDTO user)
      {
         try
         {
            var model = _mapper.Map<CareUser>(user);
            var result = await _userService.UpdateUserAsync<CareUser>(model, Guid.Parse(id));
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
