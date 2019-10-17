using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singer.Configuration;
using Singer.DTOs;
using Singer.DTOs.Users;
using Singer.Models.Users;
using Singer.Services.Interfaces;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   [Authorize]
   // TODO Fix Role based access
   [Authorize(Roles = Roles.ROLE_ADMINISTRATOR)]
   public class AdminController : DataControllerBase<AdminUser, AdminUserDTO, CreateAdminUserDTO, UpdateAdminUserDTO>
   {
      public AdminController(IAdminUserService adminUserService) : base(adminUserService)
      {
      }
   }
}
