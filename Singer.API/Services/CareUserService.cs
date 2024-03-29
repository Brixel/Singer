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
using Singer.DTOs;
using Singer.DTOs.Users;
using Singer.Helpers.Exceptions;
using Singer.Models;
using Singer.Models.Users;
using Singer.Resources;
using Singer.Services.Interfaces;

namespace Singer.Services;

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
              f.User.LastName.Contains(filter);

        return filterExpression;
    }


    public async Task AddLinkedUsers(Guid careUserId, List<Guid> newLinkedUsers)
    {
        // First check if LGUser exists
        var careUser = await Context.CareUsers.FindAsync(careUserId);
        if (careUser == null)
            throw new NotFoundException($"Tried to add user link for non existing CareUser with id {careUserId}", ErrorMessages.CareUserDoesntExist);

        var newCareUsers = new List<LegalGuardianCareUser>();
        foreach (var u in newLinkedUsers)
        {
            var legalGuardianUser = await Context.LegalGuardianUsers.FirstOrDefaultAsync(c => c.Id == u);
            if (legalGuardianUser == null)
                throw new NotFoundException($"Tried to link LG User which does not exist (id={u})", ErrorMessages.LegalGuardianDoesntExist);

            var linkedUserExists = await Context.LegalGuardianCareUsers
               .FirstOrDefaultAsync(x => x.LegalGuardianId == u && x.CareUserId == careUserId);

            if (linkedUserExists != null)
                throw new BadInputException($"Duplicate link was attempted to add: LGUserId: {u}, CareUserID: {careUserId}", ErrorMessages.DuplicateCareUserLGUserLink);

            newCareUsers.Add(new LegalGuardianCareUser() { CareUserId = careUserId, LegalGuardianId = u });
        }

        careUser.LegalGuardianCareUsers = newCareUsers;
        await Context.SaveChangesAsync();
    }

    public async Task RemoveLinkedUsers(Guid careUserId, List<Guid> usersToRemove)
    {
        // First check if LGUser exists
        var careUser = await Context.CareUsers.FindAsync(careUserId);
        if (careUser == null)
            throw new NotFoundException($"Tried to remove user link for non existing CareUser with id {careUserId}", ErrorMessages.CareUserDoesntExist);

        foreach (var u in usersToRemove)
        {
            var linkedUserExists = await Context.LegalGuardianCareUsers
               .FirstOrDefaultAsync(x => x.LegalGuardianId == u && x.CareUserId == careUserId);

            if (linkedUserExists == null)
                throw new NotFoundException($"You tried to remove a care user from a CareUser which was not linked to begin with (LG ID: {u})", ErrorMessages.LinkCareUserLGUserDoesntExist);

            careUser.LegalGuardianCareUsers.Remove(linkedUserExists);
        }

        await Context.SaveChangesAsync();
    }

    public async Task<List<EventRelevantCareUserDTO>> GetCareUsersForLegalGuardianAsync(Guid baseUserId)
    {
        var legalGuardianCareUsers = await Context.LegalGuardianCareUsers
           .Include(x => x.CareUser)
           .ThenInclude(x => x.User)
           .Where(x => x.LegalGuardian.UserId == baseUserId)
           .ToListAsync();

        var careUsers = legalGuardianCareUsers.Select(x => x.CareUser).ToList();
        return ProjectToRelevantCareUsers(careUsers);
    }

    public async Task<List<RelatedCareUserDTO>> GetRelatedCareUserAsync(Guid userId, string searchValue)
    {
        return await Context.LegalGuardianCareUsers
           .Where(x =>
              x.LegalGuardian.UserId == userId &&
              (x.CareUser.User.FirstName.Contains(searchValue) ||
               x.CareUser.User.LastName.Contains(searchValue)))
           .Select(x => new RelatedCareUserDTO
           {
               Id = x.CareUserId,
               UserId = x.CareUser.UserId,
               FirstName = x.CareUser.User.FirstName,
               LastName = x.CareUser.User.LastName,
               AgeGroup = x.CareUser.AgeGroup
           }).ToListAsync();
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
