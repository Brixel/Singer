using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Singer.Models;
namespace Singer.Services.Interfaces
{
   public interface IUserService
   {
      Task<T> CreateUserAsync<T>(T createUser) where T : User;

      Task<IList<T>> GetAllUsersAsync<T>() where T : User;

      Task<SearchResults<T>> GetUsersAsync<T>(
         int page = 0,
         int userPerPage = 15,
         StringFilter<T> filter = null,
         Sorter<T> sorter = null)
         where T : User;

      Task<T> GetUserAsync<T>(Guid id) where T : User;

      Task<bool> UpdateUserAsync<T>(T user, Guid id) where T : User;

      Task DeleteUserAsync(Guid id);
   }
}
