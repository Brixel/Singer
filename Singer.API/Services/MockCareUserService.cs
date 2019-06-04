using System.Collections.Generic;
using System.Threading.Tasks;
using Singer.Data.Models;
using Singer.DTOs;
using Singer.Services.Interfaces;

namespace Singer.Services
{
   public class MockCareUserService : ICareUsersService
   {
      #region METHODS

      public Task<CareUserDTO> CreateCareUser(CareUserDTO careUser)
      {
         throw new System.NotImplementedException();
      }

      public Task<PaginationModel<CareUserDTO>> GetAllCareUsers()
      {
         throw new System.NotImplementedException();
      }

      public Task<CareUserDTO> GetCareUser(string id)
      {
         throw new System.NotImplementedException();
      }

      public Task<CareUserDTO> UpdateCareUser(CareUserDTO careUser, string id,
         IEnumerable<string> propertiesToUpdate = null)
      {
         throw new System.NotImplementedException();
      }

      public Task DeleteCareUser(string id)
      {
         throw new System.NotImplementedException();
      }

      #endregion METHODS
   }
}
