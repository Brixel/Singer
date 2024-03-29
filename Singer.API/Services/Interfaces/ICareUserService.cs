﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Singer.DTOs;
using Singer.DTOs.Users;
using Singer.Models;
using Singer.Models.Users;

namespace Singer.Services.Interfaces;

/// <summary>
/// Interface that describes the methods implemented specifically for the CareUserService.
/// </summary>
public interface ICareUserService : IDatabaseService<CareUser, CareUserDTO, CreateCareUserDTO, UpdateCareUserDTO>
{
    /// <summary>
    /// Adds a link in the database between the given CareUserId and each of the
    /// NewLinkUsers ID's
    /// </summary>
    /// <param name="careUserId">The ID of the CareUser.</param>
    /// <param name="newLinkedUsers">
    /// A List of Guids, one for each LegalGuardianUser which should be linked to the CareUser
    /// </param>
    Task AddLinkedUsers(Guid careUserId, List<Guid> newLinkedUsers);

    /// <summary>
    /// Removes links in the database between the CareUserId and each of the
    ///  UsersToRemove ID's
    /// </summary>
    /// <param name="careUserId">The ID of the CareUser.</param>
    /// <param name="usersToRemove">
    /// A List of Guids, one for each LegalGuardianUser which should be removed fromthe CareUser
    /// </param>
    Task RemoveLinkedUsers(Guid careUserId, List<Guid> usersToRemove);
    Task<List<EventRelevantCareUserDTO>> GetCareUsersForLegalGuardianAsync(Guid legalGuardianUserId);
    Task<List<EventRelevantCareUserDTO>> GetCareUsersInAgeGroups(List<AgeGroup> singerEventAllowedAgeGroups);

    Task<List<RelatedCareUserDTO>> GetRelatedCareUserAsync(Guid userId, string searchValue);
}
