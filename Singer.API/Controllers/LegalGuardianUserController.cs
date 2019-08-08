using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs.Users;
using Singer.Helpers.Exceptions;
using Singer.Models.Users;
using Singer.Services;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   public class LegalGuardianUserController : DataControllerBase<LegalGuardianUser, LegalGuardianUserDTO, CreateLegalGuardianUserDTO>
   {
      private LegalGuardianUserService LegalGuardianUserService;
      public LegalGuardianUserController(LegalGuardianUserService databaseService) : base(databaseService)
      {
         LegalGuardianUserService = databaseService;
      }
      [HttpPut("{id}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public override async Task<ActionResult> Update(Guid id, [FromBody]LegalGuardianUserDTO dto)
      {
         if (dto is null)
         {
            throw new BadInputException(nameof(dto));
         }

         if (dto.CareUsersToAdd.Count > 0)
         {
            await LegalGuardianUserService.AddLinkedUsers(id, dto.CareUsersToAdd);
         }

         var result = await DatabaseService.UpdateAsync(id, dto);
         return Ok(result);
      }
   }
}
