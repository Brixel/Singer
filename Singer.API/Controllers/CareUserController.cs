using System;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singer.Configuration;
using Singer.DTOs.Users;
using Singer.Helpers.Exceptions;
using Singer.Models.Users;
using Singer.Services.Interfaces;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   [Authorize]
   public class CareUserController : DataControllerBase<CareUser, CareUserDTO, CreateCareUserDTO, UpdateCareUserDTO>
   {
      private readonly ICareUserService _careUserService;
      private readonly IDateValidator _dateValidator;

      public CareUserController(ICareUserService careUserService, IDateValidator dateValidator) : base(careUserService)
      {
         _careUserService = careUserService;
         _dateValidator = dateValidator;
      }

      [HttpPost]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      [Authorize(Roles = Roles.ROLE_ADMINISTRATOR)]
      public override async Task<IActionResult> Create([FromBody]CreateCareUserDTO dto)
      {
         if (dto is null)
            throw new BadInputException("DTO is null", "Er is geen data meegegeven.");
         _dateValidator.Validate(dto);

         return await base.Create(dto);
      }

      [HttpPut("{id}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      [Authorize(Roles = Roles.ROLE_ADMINISTRATOR)]
      public override async Task<IActionResult> Update(Guid id, [FromBody]UpdateCareUserDTO dto)
      {
         if (dto is null)
            throw new BadInputException("DTO is null", "Er is geen data meegegeven.");
         var model = ModelState;
         if (!model.IsValid)
            throw new BadInputException("Invalid dto", $"De data is niet geldig: {model}");
         _dateValidator.Validate(dto);

         if ((dto.LegalGuardianUsersToAdd?.Count ?? 0) > 0)
            await _careUserService.AddLinkedUsers(id, dto.LegalGuardianUsersToAdd);

         if ((dto.LegalGuardianUsersToRemove?.Count ?? 0) > 0)
            await _careUserService.RemoveLinkedUsers(id, dto.LegalGuardianUsersToRemove);

         var result = await DatabaseService.UpdateAsync(id, dto);
         return Ok(result);
      }

      [HttpGet("self")]
      public async Task<IActionResult> GetOwnCareUsers(string search)
      {
         var userId = Guid.Parse(User.GetSubjectId());

         var relatedCareUsers = await _careUserService.GetRelatedCareUserAsync(userId, search);
         return Ok(relatedCareUsers);

      }
   }
}
