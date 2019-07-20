using System;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

      // TODO: Should fix automapper config so this custom mapper is not needed
      public override Expression<Func<LegalGuardianUser, LegalGuardianUserDTO>> EntityToDTOProjector
         => x => new LegalGuardianUserDTO
         {
            Id = x.Id,
            FirstName = x.User.FirstName,
            LastName = x.User.LastName,
            Email = x.User.Email,
            Address = x.Address,
            PostalCode = x.PostalCode,
            City = x.City,
            Country = x.Country,
            CareUsers = null
         };
   }
}
