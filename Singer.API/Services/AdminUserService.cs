using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.DTOs.Users;
using Singer.Models.Users;
using Singer.Services.Interfaces;

namespace Singer.Services
{
   public class AdminUserService : UserService<AdminUser, AdminUserDTO, CreateAdminUserDTO, UpdateAdminUserDTO>, IAdminUserService
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
