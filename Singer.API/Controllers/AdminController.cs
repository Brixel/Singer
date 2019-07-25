using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs;
using Singer.DTOs.Users;
using Singer.Models.Users;
using Singer.Services.Interfaces;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   [Authorize()]
   public class AdminController : DataControllerBase<AdminUser, AdminUserDTO, CreateAdminUserDTO>
   {
      public AdminController(IDatabaseService<AdminUser, AdminUserDTO, CreateAdminUserDTO> databaseService) : base(databaseService)
      {
      }
   }
}
