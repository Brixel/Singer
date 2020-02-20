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

      public async Task<ActionResult<CareRegistrationResultDTO>> Create(
         [FromBody] CreateCareRegistrationDTO createCareRegistration)
      {
         var result = await _eventRegistrationService.Create(createCareRegistration.EventRegistrationType
            , createCareRegistration.CareUserIds, createCareRegistration.StartDateTime,
            createCareRegistration.EndDateTime);
         return new CareRegistrationResultDTO()
         {
            CreatedRegistrionIds = result
         };
      }
   }

   public class CreateCareRegistrationDTO
   {
      public IReadOnlyList<Guid> CareUserIds { get; set; }
      public DateTime StartDateTime { get; set; }
      public DateTime EndDateTime { get; set; }
      public EventRegistrationTypes EventRegistrationType { get; set; }
   }

   public class CareRegistrationResultDTO
   {
      public IReadOnlyList<Guid> CreatedRegistrionIds { get; set; }
   }
}
