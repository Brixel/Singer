using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Singer.DTOs.Users;
using Singer.Helpers.Exceptions;
using Singer.Models.Users;
using Singer.Resources;
using Singer.Services.Interfaces;

namespace Singer.Controllers;

[Route("api/[controller]")]
[Authorize]
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
    public override async Task<IActionResult> Update(Guid id, [FromBody] UpdateLegalGuardianUserDTO dto)
    {
        if (dto is null)
            throw new BadInputException("No dto was passed in the body of the request", ErrorMessages.NoDataPassed);

        var model = ModelState;
        if (!model.IsValid)
            return BadRequest(model);

        if ((dto.CareUsersToAdd?.Count ?? 0) > 0)
            await _legalGuardianUserService.AddLinkedUsers(id, dto.CareUsersToAdd);

        if ((dto.CareUsersToRemove?.Count ?? 0) > 0)
            await _legalGuardianUserService.RemoveLinkedUsers(id, dto.CareUsersToRemove);

        var result = await DatabaseService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public override async Task<IActionResult> Delete(Guid id)
    {
        await _legalGuardianUserService.ArchiveAsync(id);
        return NoContent();
    }
}
