using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs;
using Singer.Models;
using Singer.Services;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   public class LegalGuardianUserController : DataControllerBase<LegalGuardianUser, LegalGuardianUserDTO, CreateLegalGuardianUserDTO>
   {
      public LegalGuardianUserController(LegalGuardianUserService databaseService) : base(databaseService)
      {
      }
   }
}
