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
      private readonly IMapper _mapper;
      public CareUserController(IUserService service, ApplicationDbContext appContext, IMapper mapper)
      {
         _userService = service;
         _appContext = appContext;
         _mapper = mapper;
      }

      [HttpGet]
      [ProducesResponseType(StatusCodes.Status200OK)]
      public async Task<ActionResult<IEnumerable<CareUserDTO>>> GetUsers()
      {
         var users = await _userService.GetAllUsersAsync<CareUserDTO>();
         return Ok(users);
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
      public async Task<ActionResult<CareUserDTO>> CreateUser(CareUserDTO user)
      {
         if (user.Id != null)
         {
            return BadRequest();
         }
         var returnUser = await _userService.CreateUserAsync(user);
         return Created(nameof(GetUser), returnUser);
      }
   }
}
