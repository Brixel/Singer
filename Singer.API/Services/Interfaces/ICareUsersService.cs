using System.Collections.Generic;
using System.Threading.Tasks;
using Singer.Data.Models;
using Singer.DTOs;

namespace Singer.Services.Interfaces
{
   public interface ICareUsersService
   {
      Task<CareUserDTO> CreateCareUser(CareUserDTO careUser);
      Task<PaginationModel<CareUserDTO>> GetAllCareUsers();
      Task<CareUserDTO> GetCareUser(string id);
      Task<CareUserDTO> UpdateCareUser(CareUserDTO careUser, string id, IEnumerable<string> propertiesToUpdate = null);
      Task DeleteCareUser(string id);
   }
}
