using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.DTOs.Users;
using Singer.Helpers.Exceptions;
using Singer.Models.Users;
using Singer.Services.Interfaces;

namespace Singer.Services
{
   public class CareUserService : UserService<CareUser, CareUserDTO, CreateCareUserDTO, UpdateCareUserDTO>,
      ICareUserService<CareUser, CareUserDTO, CreateCareUserDTO, UpdateCareUserDTO>
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


      public async Task AddLinkedUsers(Guid CareUserId, List<Guid> NewLinkedUsers)
      {
         // First check if LGUser exists
         var CareUser = await Context.CareUsers.FindAsync(CareUserId);
         if (CareUser == null)
         {
            throw new BadInputException($"Tried to add user link for non existing CareUser with id {CareUserId}");
         }

         List<LegalGuardianCareUser> NewCareUsers = new List<LegalGuardianCareUser>();
         foreach (var u in NewLinkedUsers)
         {
            var LegalGuardianUser = await Context.LegalGuardianUsers.FirstOrDefaultAsync(c => c.Id == u);
            if (LegalGuardianUser == null)
            {
               throw new BadInputException($"Tried to link LG User which does not exist (id={u})");
            }
            var LinkedUserExists = await Context.LegalGuardianCareUsers.FirstOrDefaultAsync(
               x => x.LegalGuardianId == u
               && x.CareUserId == CareUserId
            );
            if (LinkedUserExists != null)
            {
               throw new BadInputException($"Duplicate link was attempted to add: LGUserId: {u}, CareUserID: {CareUserId}");
            }
            NewCareUsers.Add(new LegalGuardianCareUser() { CareUserId = CareUserId, LegalGuardianId = u });
         }
         CareUser.LegalGuardianCareUsers = NewCareUsers;
         await Context.SaveChangesAsync();
      }

      public async Task RemoveLinkedUsers(Guid CareUserId, List<Guid> UsersToRemove)
      {
         // First check if LGUser exists
         var CareUser = await Context.CareUsers.FindAsync(CareUserId);
         if (CareUser == null)
         {
            throw new BadInputException($"Tried to remove user link for non existing CareUser with id {CareUserId}");
         }

         foreach (var u in UsersToRemove)
         {
            var LinkedUserExists = await Context.LegalGuardianCareUsers.FirstOrDefaultAsync(
               x => x.LegalGuardianId == u
               && x.CareUserId == CareUserId
            );
            if (LinkedUserExists == null)
            {
               throw new BadInputException($"You tried to remove a care user from a CareUser which was not linked to begin with (LG ID: {u})");
            }
            CareUser.LegalGuardianCareUsers.Remove(LinkedUserExists);
         }
         await Context.SaveChangesAsync();
      }
   }
}
