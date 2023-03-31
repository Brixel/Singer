using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Singer.DTOs;
using Singer.Services.Interfaces;

namespace Singer.Controllers;

[Authorize]
[Route("api/careregistration")]
public class CareRegistrationController : Controller
{
    private readonly IEventRegistrationService _eventRegistrationService;

    public CareRegistrationController(IEventRegistrationService eventRegistrationService)
    {
        _eventRegistrationService = eventRegistrationService;
    }

    [HttpPost]
    public async Task<ActionResult<CareRegistrationResultDTO>> Create(
       [FromBody] CreateCareRegistrationDTO createCareRegistration)
    {
        var result = await _eventRegistrationService.Create(createCareRegistration.EventRegistrationType
           , createCareRegistration.CareUserIds, createCareRegistration.StartDateTime.LocalDateTime,
           createCareRegistration.EndDateTime.LocalDateTime);
        return new CareRegistrationResultDTO()
        {
            CreatedRegistrationIds = result
        };
    }
}
