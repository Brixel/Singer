using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Singer.Configuration;
using Singer.Data;
using Singer.DTOs;
using Singer.DTOs.Users;
using Singer.Models.Users;

namespace Singer.Services
{
   public class AdminUserService : UserService<AdminUser, AdminUserDTO, CreateAdminUserDTO, UpdateAdminUserDTO>
   {
      private readonly RoleManager<IdentityRole<Guid>> _roleManager;

      public AdminUserService(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager) : base(context, mapper, userManager)
      {
         _roleManager = roleManager;
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

      public override async Task<AdminUserDTO> CreateAsync(
         CreateAdminUserDTO dto,
         Expression<Func<CreateAdminUserDTO, AdminUser>> dtoToEntityProjector = null,
         Expression<Func<AdminUser, AdminUserDTO>> entityToDTOProjector = null)
      {
         var result = await base.CreateAsync(dto, dtoToEntityProjector, entityToDTOProjector);
         var createdUser = await UserManager.FindByEmailAsync(result.Email);
         await UserManager.AddToRoleAsync(createdUser, Roles.ROLE_ADMINISTRATOR);
         await UserManager.AddClaimAsync(createdUser, new Claim(ClaimTypes.Role, Roles.ROLE_ADMINISTRATOR));
         return result;
      }
   }
}
