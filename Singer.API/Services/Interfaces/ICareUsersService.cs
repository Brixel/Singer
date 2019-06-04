using System.Collections.Generic;
using System.Threading.Tasks;
using Singer.Data.Models;
using Singer.DTOs;

namespace Singer.Services.Interfaces
{
   public interface ICareUsersService
   {
      Task<CareUserDTO> CreateCareUserAsync(CareUserDTO careUser);
      Task<IList<CareUserDTO>> GetAllCareUsersAsync();
      Task<CareUserDTO> GetCareUserAsync(string id);
      Task<CareUserDTO> UpdateCareUserAsync(CareUserDTO careUser, string id, IList<string> propertiesToUpdate = null);
      Task DeleteCareUserAsync(string id);
   }
}
