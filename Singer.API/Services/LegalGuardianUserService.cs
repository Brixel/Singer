using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Singer.Data;
using Singer.DTOs.Users;
using Singer.Models.Users;

namespace Singer.Services
{
   public class LegalGuardianUserService : UserService<LegalGuardianUser, LegalGuardianUserDTO, CreateLegalGuardianUserDTO>
   {
      public LegalGuardianUserService(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
      : base(context, mapper, userManager)
      {
      }

      protected override DbSet<LegalGuardianUser> DbSet => Context.LegalGuardianUsers;

      protected override IQueryable<LegalGuardianUser> Queryable => Context.LegalGuardianUsers.Include( x=>x.User);

      protected override Expression<Func<LegalGuardianUser, bool>> Filter(string filter)
      {
         if (string.IsNullOrWhiteSpace(filter))
         {
            return o => true;
         }
         Expression<Func<LegalGuardianUser, bool>> filterExpression =
            f =>
               f.User.FirstName.Contains(filter) ||
               f.User.LastName.Contains(filter);
         return filterExpression;
      }
   }
}
