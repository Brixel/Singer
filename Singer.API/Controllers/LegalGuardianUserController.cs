using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs.Users;
using Singer.Helpers.Exceptions;
using Singer.Models.Users;
using Singer.Services;
using Singer.Services.Interfaces;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   public class LegalGuardianUserController : DataControllerBase<LegalGuardianUser, LegalGuardianUserDTO, CreateLegalGuardianUserDTO, UpdateLegalGuardianUserDTO>
   {
      private readonly ILegalGuardianUserService _legalGuardianUserService;
      public LegalGuardianUserController(ILegalGuardianUserService legalGuardianUserService) : base(legalGuardianUserService)
      {
         _legalGuardianUserService = legalGuardianUserService;
      }
      [HttpPut("{id}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public override async Task<IActionResult> Update(Guid id, [FromBody]UpdateLegalGuardianUserDTO dto)
      {
         if (dto is null)
         {
            throw new BadInputException(nameof(dto));
         }

         if ((dto.CareUsersToAdd?.Count ?? 0) > 0)
         {
            await _legalGuardianUserService.AddLinkedUsers(id, dto.CareUsersToAdd);
         }

         if ((dto.CareUsersToRemove?.Count ?? 0) > 0)
         {
            await _legalGuardianUserService.RemoveLinkedUsers(id, dto.CareUsersToRemove);
         }

         var result = await DatabaseService.UpdateAsync(id, dto);
         return Ok(result);
      }
   }
}
