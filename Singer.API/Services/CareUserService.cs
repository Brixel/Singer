using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Singer.Data;
using Singer.Data.Models.Configuration;
using Singer.DTOs.Users;
using Singer.DTOs;
using Singer.Helpers.Exceptions;
using Singer.Models;
using Singer.Models.Users;
using Singer.Services.Interfaces;
using Singer.Resources;

namespace Singer.Services
{
   public class CareUserService : UserService<CareUser, CareUserDTO, CreateCareUserDTO, UpdateCareUserDTO>,
      ICareUserService
   {
      protected override DbSet<CareUser> DbSet => Context.CareUsers;

      protected override IQueryable<CareUser> Queryable => Context.CareUsers
         .Include(x => x.User)
         .Include(x => x.LegalGuardianCareUsers)
            .ThenInclude(x => x.LegalGuardian)
            .ThenInclude(x => x.User)
         .AsQueryable();

      public CareUserService(ApplicationDbContext appContext, IMapper mapper, UserManager<User> userManager,
         IOptions<ApplicationConfig> applicationConfigurationOptions)
      : base(appContext, mapper, userManager, null, applicationConfigurationOptions)
      {
      }
      protected override Expression<Func<CareUser, bool>> Filter(string filter)
      {
         if (string.IsNullOrWhiteSpace(filter))
            return o => true;

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
         var careUser = await Context.CareUsers.FindAsync(CareUserId);
         if (careUser == null)
            throw new NotFoundException($"Tried to add user link for non existing CareUser with id {CareUserId}", ErrorMessages.CareUserDoesntExist);

         List<LegalGuardianCareUser> NewCareUsers = new List<LegalGuardianCareUser>();
         foreach (var u in NewLinkedUsers)
         {
            var legalGuardianUser = await Context.LegalGuardianUsers.FirstOrDefaultAsync(c => c.Id == u);
            if (legalGuardianUser == null)
               throw new NotFoundException($"Tried to link LG User which does not exist (id={u})", ErrorMessages.LegalGuardianDoesntExist);

            var linkedUserExists = await Context.LegalGuardianCareUsers
               .FirstOrDefaultAsync(x => x.LegalGuardianId == u && x.CareUserId == CareUserId);

            if (linkedUserExists != null)
               throw new BadInputException($"Duplicate link was attempted to add: LGUserId: {u}, CareUserID: {CareUserId}", ErrorMessages.DuplicateCareUserLGUserLink);

            NewCareUsers.Add(new LegalGuardianCareUser() { CareUserId = CareUserId, LegalGuardianId = u });
         }

         careUser.LegalGuardianCareUsers = NewCareUsers;
         await Context.SaveChangesAsync();
      }

      public async Task RemoveLinkedUsers(Guid CareUserId, List<Guid> UsersToRemove)
      {
         // First check if LGUser exists
         var careUser = await Context.CareUsers.FindAsync(CareUserId);
         if (careUser == null)
            throw new NotFoundException($"Tried to remove user link for non existing CareUser with id {CareUserId}", ErrorMessages.CareUserDoesntExist);

         foreach (var u in UsersToRemove)
         {
            var linkedUserExists = await Context.LegalGuardianCareUsers
               .FirstOrDefaultAsync(x => x.LegalGuardianId == u && x.CareUserId == CareUserId);

            if (linkedUserExists == null)
               throw new NotFoundException($"You tried to remove a care user from a CareUser which was not linked to begin with (LG ID: {u})", ErrorMessages.LinkCareUserLGUserDoesntExist);

            careUser.LegalGuardianCareUsers.Remove(linkedUserExists);
         }

         await Context.SaveChangesAsync();
      }

      public async Task<List<EventRelevantCareUserDTO>> GetCareUsersForLegalGuardian(Guid baseUserId)
      {
         var legalGuardianCareUsers = await Context.LegalGuardianCareUsers
            .Include(x => x.CareUser)
            .ThenInclude(x => x.User)
            .Where(x => x.LegalGuardian.UserId == baseUserId)
            .ToListAsync();

         var careUsers = legalGuardianCareUsers.Select(x => x.CareUser).ToList();
         return ProjectToRelevantCareUsers(careUsers);
      }


      public async Task<List<EventRelevantCareUserDTO>> GetCareUsersInAgeGroups(List<AgeGroup> ageGroups)
      {
         var careUsers = await Context.CareUsers
            .Include(x => x.User)
            .Where(x => ageGroups.Contains(x.AgeGroup))
            .ToListAsync();

         return ProjectToRelevantCareUsers(careUsers, true);
      }

      private static List<EventRelevantCareUserDTO> ProjectToRelevantCareUsers(
         List<CareUser> careUsers,
         bool isAppropriateAgeGroup = false)
      {
         return careUsers.Select(careUser => new EventRelevantCareUserDTO()
         {
            Id = careUser.Id,
            UserId = careUser.UserId,
            FirstName = careUser.User.FirstName,
            LastName = careUser.User.LastName,
            AgeGroup = careUser.AgeGroup,
            AppropriateAgeGroup = isAppropriateAgeGroup,
            BirthDay = careUser.BirthDay
         }).ToList();
      }
   }
}
