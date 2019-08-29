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

namespace Singer.Services
{
   public class LegalGuardianUserService : UserService<LegalGuardianUser, LegalGuardianUserDTO, CreateLegalGuardianUserDTO>
   {
      public LegalGuardianUserService(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
      : base(context, mapper, userManager)
      {
      }

      protected override DbSet<LegalGuardianUser> DbSet => Context.LegalGuardianUsers;

      protected override IQueryable<LegalGuardianUser> Queryable => Context.LegalGuardianUsers.Include(x => x.User);

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

      public async Task AddLinkedUsers(Guid LegalGuardianUserId, List<Guid> NewLinkedUsers)
      {
         // First check if LGUser exists
         var LegalGuardianUser = await Context.LegalGuardianUsers.FindAsync(LegalGuardianUserId);
         if (LegalGuardianUser == null)
         {
            throw new BadInputException($"Tried to add user link for non existing LG User with id {LegalGuardianUserId}");
         }

         List<LegalGuardianCareUser> NewCareUsers = new List<LegalGuardianCareUser>();
         foreach (var u in NewLinkedUsers)
         {
            var CareUser = await Context.CareUsers.FirstOrDefaultAsync(c => c.Id == u);
            if (CareUser == null)
            {
               throw new BadInputException($"Tried to link careuser which does not exist (id={u})");
            }
            var LinkedUserExists = await Context.LegalGuardianCareUsers.FirstOrDefaultAsync(
               x => x.CareUserId == u
               && x.LegalGuardianId == LegalGuardianUserId
            );
            if (LinkedUserExists != null)
            {
               throw new BadInputException($"Duplicate link was attempted to add: LGUserId: {LegalGuardianUserId}, CareUserID: {u}");
            }
            NewCareUsers.Add(new LegalGuardianCareUser() { CareUserId = u, LegalGuardianId = LegalGuardianUserId });
         }
         LegalGuardianUser.LegalGuardianCareUsers = NewCareUsers;
         await Context.SaveChangesAsync();
      }

      public async Task RemoveLinkedUsers(Guid LegalGuardianUserId, List<Guid> UsersToRemove)
      {
         // First check if LGUser exists
         var LegalGuardianUser = await Context.LegalGuardianUsers.FindAsync(LegalGuardianUserId);
         if (LegalGuardianUser == null)
         {
            throw new BadInputException($"Tried to remove user link for non existing LG User with id {LegalGuardianUserId}");
         }

         foreach (var u in UsersToRemove)
         {
            var LinkedUserExists = await Context.LegalGuardianCareUsers.FirstOrDefaultAsync(
               x => x.CareUserId == u
               && x.LegalGuardianId == LegalGuardianUserId
            );
            if (LinkedUserExists == null)
            {
               throw new BadInputException($"You tried to remove a care user from an LG user which was not linked to begin with (CareUser ID: {u})");
            }
            LegalGuardianUser.LegalGuardianCareUsers.Remove(LinkedUserExists);
         }
         await Context.SaveChangesAsync();
      }
   }
}
