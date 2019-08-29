using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Singer.Helpers;

namespace Singer.Services.Interfaces
{
   /// <summary>
   /// Interface that describes the methods implemented specifically for the CareUserService.
   /// </summary>
   /// <typeparam name="TEntity">The type of the entity to manipulate in the database.</typeparam>
   /// <typeparam name="TDTO">The type that will be exposed to the outside world.</typeparam>
   /// <typeparam name="TCreateDTO">The type that is used to create new entities in the database.</typeparam>
   /// <typeparam name="TUpdateDTO">The type that is used to update entities in the database.</typeparam>
   public interface ICareUserService<TEntity, TDTO, TCreateDTO, TUpdateDTO> : IDatabaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO>
      where TEntity : class, IIdentifiable
      where TDTO : class, IIdentifiable
      where TCreateDTO : class
      where TUpdateDTO : class
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
