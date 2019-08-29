using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs.Users;
using Singer.Models.Users;
using Singer.Helpers.Exceptions;
using Singer.Services.Interfaces;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   //[Authorize()]
   public class CareUserController : DataControllerBase<CareUser, CareUserDTO, CreateCareUserDTO, UpdateCareUserDTO>
   {
      private readonly ICareUserService careUserService;
      public CareUserController(ICareUserService databaseService) : base(databaseService)
      {
         careUserService = databaseService;
      }

      [HttpPut("{id}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public override async Task<ActionResult> Update(Guid id, [FromBody]UpdateCareUserDTO dto)
      {
         if (dto is null)
         {
            throw new BadInputException(nameof(dto));
         }

         if ((dto.LegalGuardianUsersToAdd?.Count ?? 0) > 0)
         {
            await careUserService.AddLinkedUsers(id, dto.LegalGuardianUsersToAdd);
         }

         if ((dto.LegalGuardianUsersToRemove?.Count ?? 0) > 0)
         {
            await careUserService.RemoveLinkedUsers(id, dto.LegalGuardianUsersToRemove);
         }

         var result = await DatabaseService.UpdateAsync(id, dto);
         return Ok(result);
      }
   }
}
