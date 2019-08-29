using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs.Users;
using Singer.DTOs;
using Singer.Models.Users;
using Singer.Services;
using Singer.Helpers.Exceptions;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   //[Authorize()]
   public class CareUserController : DataControllerBase<CareUser, CareUserDTO, CreateCareUserDTO, UpdateCareUserDTO>
   {
      private readonly CareUserService careUserService;
      public CareUserController(CareUserService databaseService) : base(databaseService)
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
