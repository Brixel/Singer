using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Singer.DTOs.Users;
using Singer.Helpers;
using Singer.Models.Users;

namespace Singer.Services.Interfaces
{
   /// <summary>
   /// Interface that describes the methods implemented specifically for the CareUserService.
   /// </summary>
   /// <typeparam name="TEntity">The type of the entity to manipulate in the database.</typeparam>
   /// <typeparam name="TDTO">The type that will be exposed to the outside world.</typeparam>
   /// <typeparam name="TCreateDTO">The type that is used to create new entities in the database.</typeparam>
   /// <typeparam name="TUpdateDTO">The type that is used to update entities in the database.</typeparam>
   public interface ILegalGuardianUserService : IDatabaseService<LegalGuardianUser, LegalGuardianUserDTO, CreateLegalGuardianUserDTO, UpdateLegalGuardianUserDTO>
   {
      /// <summary>
      /// Adds a link in the database between the given LegalGuardianUserId and each of the
      /// NewLinkUsers ID's
      /// </summary>
      /// <param name="legalGuardianUserId">The ID of the LegalGuardianUser.</param>
      /// <param name="newLinkedUsers">
      /// A List of Guids, one for each CareUser which should be linked to the LegalGuardianUser
      /// </param>
      Task AddLinkedUsers(Guid legalGuardianUserId, List<Guid> newLinkedUsers);

      /// <summary>
      /// Removes links in the database between the LegalGuardianUserId and each of the
      ///  UsersToRemove ID's
      /// </summary>
      /// <param name="legalGuardianUserId">The ID of the CareUser.</param>
      /// <param name="usersToRemove">
      /// A List of Guids, one for each CareUser which should be removed from the LegalGuardianUser
      /// </param>
      Task RemoveLinkedUsers(Guid legalGuardianUserId, List<Guid> usersToRemove);
   }
}
