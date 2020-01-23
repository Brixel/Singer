using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Singer.Data;
using Singer.Data.Models.Configuration;
using Singer.DTOs.Users;
using Singer.Helpers.Exceptions;
using Singer.Models.Users;
using Singer.Resources;
using Singer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Singer.Services
{
   public class LegalGuardianUserService : UserService<LegalGuardianUser, LegalGuardianUserDTO, CreateLegalGuardianUserDTO, UpdateLegalGuardianUserDTO>,
      ILegalGuardianUserService
   {
      public LegalGuardianUserService(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager,
         IEmailService<LegalGuardianUserDTO> emailService, IOptions<ApplicationConfig> applicationConfigurationOptions)
      : base(context, mapper, userManager, emailService, applicationConfigurationOptions)
      {
      }

      protected override DbSet<LegalGuardianUser> DbSet => Context.LegalGuardianUsers;

      protected override IQueryable<LegalGuardianUser> Queryable => Context.LegalGuardianUsers
         .Include(x => x.User)
         .Include(x => x.LegalGuardianCareUsers).ThenInclude(x => x.CareUser)
         .AsQueryable();

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

      public async Task AddLinkedUsers(Guid legalGuardianUserId, List<Guid> newLinkedUsers)
      {
         // First check if LGUser exists
         var LegalGuardianUser = await Context.LegalGuardianUsers.FindAsync(legalGuardianUserId);
         if (LegalGuardianUser == null)
            throw new NotFoundException($"Tried to add user link for non existing LG User with id {legalGuardianUserId}", ErrorMessages.LegalGuardianDoesntExist);

         List<LegalGuardianCareUser> NewCareUsers = new List<LegalGuardianCareUser>();
         foreach (var u in newLinkedUsers)
         {
            var CareUser = await Context.CareUsers.FirstOrDefaultAsync(c => c.Id == u);
            if (CareUser == null)
               throw new NotFoundException($"Tried to link careuser which does not exist (id={u})", ErrorMessages.CareUserDoesntExist);

            var LinkedUserExists = await Context.LegalGuardianCareUsers
               .FirstOrDefaultAsync(x => x.CareUserId == u && x.LegalGuardianId == legalGuardianUserId);

            if (LinkedUserExists != null)
               throw new BadInputException($"Duplicate link was attempted to add: LGUserId: {legalGuardianUserId}, CareUserID: {u}", ErrorMessages.DuplicateCareUserLGUserLink);

            NewCareUsers.Add(new LegalGuardianCareUser() { CareUserId = u, LegalGuardianId = legalGuardianUserId });
         }

         LegalGuardianUser.LegalGuardianCareUsers = NewCareUsers;
         await Context.SaveChangesAsync();
      }

      public async Task RemoveLinkedUsers(Guid legalGuardianUserId, List<Guid> usersToRemove)
      {
         // First check if LGUser exists
         var legalGuardianUser = await Context.LegalGuardianUsers.FindAsync(legalGuardianUserId);
         if (legalGuardianUser == null)
            throw new NotFoundException($"Tried to remove user link for non existing LG User with id {legalGuardianUserId}", ErrorMessages.LegalGuardianDoesntExist);

         foreach (var u in usersToRemove)
         {
            var LinkedUserExists = await Context.LegalGuardianCareUsers
               .FirstOrDefaultAsync(x => x.CareUserId == u && x.LegalGuardianId == legalGuardianUserId);

            if (LinkedUserExists == null)
               throw new NotFoundException($"You tried to remove a care user from an LG user which was not linked to begin with (CareUser ID: {u})", ErrorMessages.LinkCareUserLGUserDoesntExist);

            legalGuardianUser.LegalGuardianCareUsers.Remove(LinkedUserExists);
         }

         await Context.SaveChangesAsync();
      }
   }
}
