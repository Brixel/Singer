using System.Collections.Generic;
using System.Threading.Tasks;
using Singer.Data.Models;
using Singer.DTOs;
using Singer.Models;

namespace Singer.Services.Interfaces
{
   public interface IUserService
   {
      Task<T> CreateCareUserAsync<T>(T careUser) where T : UserDTO;
      Task<IList<T>> GetAllCareUsersAsync<T>() where T : UserDTO;
      Task<T> GetCareUserAsync<T>(string id) where T : UserDTO;
      Task<T> UpdateCareUserAsync<T>(T careUser, string id, IList<string> propertiesToUpdate = null) where T : UserDTO;
      Task DeleteCareUserAsync(string id);
   }
}
