using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Singer.Models;
using Singer.Services.Interfaces;

namespace Singer.Controllers
{
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

   public class CreateCareRegistrationDTO
   {
      public IReadOnlyList<Guid> CareUserIds { get; set; }
      public DateTimeOffset StartDateTime { get; set; }
      public DateTimeOffset EndDateTime { get; set; }
      public EventRegistrationTypes EventRegistrationType { get; set; }
   }

   public class CareRegistrationResultDTO
   {
      public IReadOnlyList<Guid> CreatedRegistrationIds { get; set; }
   }
}
