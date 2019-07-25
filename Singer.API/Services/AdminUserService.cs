using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.DTOs;
using Singer.DTOs.Users;
using Singer.Models.Users;

namespace Singer.Services
{
   public class AdminUserService : UserService<AdminUser, AdminUserDTO, CreateAdminUserDTO>
   {
      public AdminUserService(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager) : base(context, mapper, userManager)
      {
      }

      protected override DbSet<AdminUser> DbSet => Context.AdminUsers;
      protected override IQueryable<AdminUser> Queryable => Context.AdminUsers.Include(x => x.User);
      protected override Expression<Func<AdminUser, bool>> Filter(string filter)
      {
         if (string.IsNullOrWhiteSpace(filter))
         {
            return o => true;
         }
         Expression<Func<AdminUser, bool>> filterExpression =
            f =>
               f.User.FirstName.Contains(filter) ||
               f.User.LastName.Contains(filter);
         return filterExpression;
      }
   }
}
