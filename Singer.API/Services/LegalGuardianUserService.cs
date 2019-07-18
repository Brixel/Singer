using System;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.DTOs;
using Singer.Models;

namespace Singer.Services
{
   public class LegalGuardianUserService : DatabaseService<LegalGuardianUser, LegalGuardianUserDTO, CreateLegalGuardianUserDTO>
   {
      public LegalGuardianUserService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
      {
      }

      protected override DbSet<LegalGuardianUser> DbSet => Context.LegalGuardianUsers;

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
