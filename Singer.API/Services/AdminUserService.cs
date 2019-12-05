using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Singer.Configuration;
using Singer.Data;
using Singer.DTOs.Users;
using Singer.Models.Users;
using Singer.Services.Interfaces;

namespace Singer.Services
{
   public class AdminUserService : UserService<AdminUser, AdminUserDTO, CreateAdminUserDTO, UpdateAdminUserDTO>, IAdminUserService
   {
      public AdminUserService(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager, IPasswordGenerator passwordGenerator, IEmailService<AdminUserDTO> emailService)
      : base(context, mapper, userManager, passwordGenerator, emailService)
      {
      }

      protected override DbSet<AdminUser> DbSet => Context.AdminUsers;
      protected override IQueryable<AdminUser> Queryable =>
         Context.AdminUsers
            .Include(x => x.User)
            .AsQueryable();

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

      public override async Task<AdminUserDTO> CreateAsync(
         CreateAdminUserDTO dto)
      {
         var result = await base.CreateAsync(dto);
         var createdUser = await UserManager.FindByEmailAsync(result.Email);
         await UserManager.AddToRoleAsync(createdUser, Roles.ROLE_ADMINISTRATOR);
         await UserManager.AddClaimAsync(createdUser, new Claim(ClaimTypes.Role, Roles.ROLE_ADMINISTRATOR));
         return result;
      }
   }
}
