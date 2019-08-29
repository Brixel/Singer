using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.DTOs.Users;
using Singer.Models.Users;

namespace Singer.Services
{
   public class CareUserService : UserService<CareUser, CareUserDTO, CreateCareUserDTO, UpdateCareUserDTO>
   {
      protected override DbSet<CareUser> DbSet => Context.CareUsers;

      protected override IQueryable<CareUser> Queryable => Context.CareUsers.Include(x => x.User);

      public CareUserService(ApplicationDbContext appContext, IMapper mapper, UserManager<User> userManager)
      : base(appContext, mapper, userManager)
      {
      }
      protected override Expression<Func<CareUser, bool>> Filter(string filter)
      {
         if (string.IsNullOrWhiteSpace(filter))
         {
            return o => true;
         }
         Expression<Func<CareUser, bool>> filterExpression =
            f =>
               f.User.FirstName.Contains(filter) ||
               f.User.LastName.Contains(filter) ||
               f.CaseNumber.Contains(filter);
         return filterExpression;
      }
   }
}
