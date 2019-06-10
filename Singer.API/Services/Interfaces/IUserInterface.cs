using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Singer.Data.Models;
using Singer.DTOs;

namespace Singer.Services.Interfaces
{
   public interface IUserService
   {
      Task<T2> CreateUserAsync<T1, T2>(T1 user)
         where T1 : CreateUserDTO
         where T2 : UserDTO;

      Task<IList<T>> GetAllUsersAsync<T>() where T : UserDTO;

      Task<PaginationModel<T>> GetUsersAsync<T>(
         int page = 0,
         Filter<T> filter = null,
         Sorter<T> sorter = null)
         where T : UserDTO;

      Task<T> GetUserAsync<T>(Guid id) where T : UserDTO;

      Task<T> UpdateUserAsync<T>(T user, Guid id, IList<string> propertiesToUpdate = null) where T : UserDTO;

      Task DeleteUserAsync(Guid id);
   }
}
