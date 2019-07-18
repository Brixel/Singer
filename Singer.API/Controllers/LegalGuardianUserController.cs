using Microsoft.AspNetCore.Mvc;
using Singer.DTOs.Users;
using Singer.Models.Users;
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
