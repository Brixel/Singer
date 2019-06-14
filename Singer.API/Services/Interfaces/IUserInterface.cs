using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Singer.DTOs;
using Singer.Models;
namespace Singer.Services.Interfaces
{
   public interface IUserService
   {
      Task<T> CreateUserAsync<T>(T createUser) where T : CareUser;

      Task<IList<T>> GetAllUsersAsync<T>() where T : CareUser;

      Task<SearchResults<CareUserDTO>> GetUsersAsync<T>(
         int page = 0,
         int userPerPage = 15,
         StringFilter<T> filter = null,
         Sorter<T> sorter = null)
         where T : CareUser;

      Task<T> GetUserAsync<T>(Guid id) where T : CareUser;

      Task<bool> UpdateUserAsync<T>(T user, Guid id) where T : CareUser;

      Task DeleteUserAsync(Guid id);
   }
}
