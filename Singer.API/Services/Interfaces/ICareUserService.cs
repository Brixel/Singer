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
   public interface ICareUserService : IDatabaseService<CareUser, CareUserDTO, CreateCareUserDTO, UpdateCareUserDTO>
   {
      /// <summary>
      /// Adds a link in the database between the given CareUserId and each of the
      /// NewLinkUsers ID's
      /// </summary>
      /// <param name="CareUserId">The ID of the CareUser.</param>
      /// <param name="NewLinkedUsers">
      /// A List of Guids, one for each LegalGuardianUser which should be linked to the CareUser
      /// </param>
      Task AddLinkedUsers(Guid CareUserId, List<Guid> NewLinkedUsers);

      /// <summary>
      /// Removes links in the database between the CareUserId and each of the
      ///  UsersToRemove ID's
      /// </summary>
      /// <param name="CareUserId">The ID of the CareUser.</param>
      /// <param name="UsersToRemove">
      /// A List of Guids, one for each LegalGuardianUser which should be removed fromthe CareUser
      /// </param>
      Task RemoveLinkedUsers(Guid CareUserId, List<Guid> UsersToRemove);
   }
}
