using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Singer.Configuration;
using Singer.Data;
using Singer.DTOs;
using Singer.DTOs.Users;
using Singer.Models.Users;
using Singer.Services;
using Singer.Services.Interfaces;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   [Authorize(Roles = Roles.ROLE_ADMINISTRATOR)]
   public class AdminController : DataControllerBase<AdminUser, AdminUserDTO, CreateAdminUserDTO, UpdateAdminUserDTO>
   {
      public AdminController(IAdminUserService adminUserService) : base(adminUserService)
      {
      }
   }

   public interface
      IAdminUserService : IDatabaseService<AdminUser, AdminUserDTO, CreateAdminUserDTO, UpdateAdminUserDTO>
   {

   }

   public class AdminUserService : UserService<AdminUser, AdminUserDTO, CreateAdminUserDTO, UpdateAdminUserDTO> ,IAdminUserService
   {
      public AdminUserService(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager) : base(context, mapper, userManager)
      {
      }

      protected override DbSet<AdminUser> DbSet => Context.AdminUsers;
      protected override IQueryable<AdminUser> Queryable => Context.AdminUsers;
      protected override Expression<Func<AdminUser, bool>> Filter(string filter)
      {
         return o => true;
      }
   }
}
