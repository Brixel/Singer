using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs;
using Singer.Models;
using Singer.Services.Interfaces;

namespace Singer.Controllers {
   [Route ("api/[controller]")]
   public class LegalGuardianUserController : Controller {
      private readonly IUserService _userService;
      private readonly IMapper _mapper;
      private readonly List<LegalGuardianUserDTO> _mockData = new List<LegalGuardianUserDTO> () {
         new LegalGuardianUserDTO {
         Id = Guid.NewGuid (),
         UserId = Guid.NewGuid (),
         /* FirstName = "Papa",
         LastName = "Van Joske", */
         FirstName = "Papa",
         LastName = "Van Joske",
         Email = "papa.vanjoske@me.be",
         UserName = "papa.vanjoske",
         CareUsers = new List<Guid> () {
         Guid.NewGuid (),
         Guid.NewGuid ()
         }
         },
         new LegalGuardianUserDTO {
         Id = Guid.NewGuid (),
         UserId = Guid.NewGuid (),
         /* FirstName = "Mama",
         LastName = "Van Joske", */
         FirstName = "Mama",
         LastName = "Van Joske",
         Email = "mama.vanjoske@me.be",
         UserName = "mama.vanjoske",
         CareUsers = new List<Guid> () {
         Guid.NewGuid (),
         Guid.NewGuid ()
         }
         },
         new LegalGuardianUserDTO {
         Id = Guid.NewGuid (),
         UserId = Guid.NewGuid (),
         /* FirstName = "Voogd",
         LastName = "Van Bram", */
         FirstName = "Voogd",
         LastName = "Van Bram",
         Email = "voogd.vanbram@me.be",
         UserName = "voogs.vanbram",
         CareUsers = new List<Guid> () {
         Guid.NewGuid (),
         Guid.NewGuid ()
         }
         }
      };

      public LegalGuardianUserController (IUserService service, IMapper mapper) {
         _userService = service;
         _mapper = mapper;
      }

      [HttpGet]
      [ProducesResponseType (StatusCodes.Status200OK)]
      public async Task<ActionResult<PaginationDTO<LegalGuardianUserDTO>>> GetUsers (string sortDirection, string sortColumn, int pageIndex, int pageSize, string filter) {
         pageIndex++;
         var users = _mockData.Where (u => filter == null || $"{u.FirstName} {u.LastName}".ToLower ().Contains (filter.ToLower ()));
         var result = new SearchResults<LegalGuardianUserDTO> (
            users.ToList (),
            users.Count (),
            pageIndex
         );
         var requestPath = HttpContext.Request.Path;
         var nextPage = (pageIndex * pageSize) + result.Size >= result.TotalCount ?
            null :
            $"{requestPath}?PageIndex={pageIndex + pageSize}&Size={pageSize}";

         var page = new PaginationDTO<LegalGuardianUserDTO> {
            Items = result.Items,
            Size = result.Items.Count,
            PageIndex = pageIndex,
            CurrentPageUrl = $"{requestPath}?{HttpContext.Request.QueryString.ToString()}",
            NextPageUrl = nextPage,
            PreviousPageUrl = pageIndex == 0 ?
            null : $"{requestPath}?PageIndex={pageIndex - pageSize}&Size={pageSize}",
            TotalSize = result.TotalCount
         };
         return Ok (page);
      }

      [HttpGet ("{id}")]
      [ProducesResponseType (StatusCodes.Status404NotFound)]
      [ProducesResponseType (StatusCodes.Status200OK)]
      public async Task<ActionResult<LegalGuardianUserDTO>> GetUser (Guid id) {
         //var user = await _userService.GetUserAsync<CareUser>(id);
         var user = _mockData.Where (u => u.UserId == id);
         if (user == null) {
            return NotFound ();
         }
         return Ok (user);
      }

      [HttpPost]
      [ProducesResponseType (StatusCodes.Status201Created)]
      [ProducesResponseType (StatusCodes.Status400BadRequest)]
      public async Task<ActionResult<LegalGuardianUserDTO>> CreateUser (CreateLegalGuardianUserDTO user) {
         /* var model = _mapper.Map<CareUser>(user);
         var returnUser = await _userService.CreateUserAsync(model);
         return Created(nameof(GetUser), returnUser); */
         return Created (nameof (GetUser), user);

      }

      [HttpPut ("{id}")]
      [ProducesResponseType (StatusCodes.Status204NoContent)]
      [ProducesResponseType (StatusCodes.Status400BadRequest)]
      public async Task<ActionResult> UpdateUser (string id, LegalGuardianUserDTO user) {
         /*          try
                  {
                     var
                        model = _mapper.Map<CareUser>(user);
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
                  } */
         return NoContent ();
      }

      [HttpDelete ("{id}")]
      [ProducesResponseType (StatusCodes.Status204NoContent)]
      [ProducesResponseType (StatusCodes.Status400BadRequest)]
      public async Task<ActionResult> DeleteUser (string id) {
         try {
            await _userService.DeleteUserAsync (Guid.Parse (id));
         } catch {
            return BadRequest ();
         }
         return NoContent ();
      }
   }
}
