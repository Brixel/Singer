using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Singer.Data.Models;
using Singer.DTOs;

namespace Singer.Services.Interfaces
{
   public interface IUserService
   {
      Task<TReturn> CreateUserAsync<TCreate, TReturn>(TCreate createUser)
         where TCreate : CreateUserDTO
         where TReturn : IUserDTO, TCreate;

      Task<IList<T>> GetAllUsersAsync<T>() where T : IUserDTO;

      Task<PaginationModel<T>> GetUsersAsync<T>(
         int page = 0,
         int elementsPerPage = 15,
         Filter<T> filter = null,
         Sorter<T> sorter = null)
         where T : IUserDTO;

      Task<T> GetUserAsync<T>(Guid id) where T : IUserDTO;

      Task<T> UpdateUserAsync<T>(T user, Guid id, IList<string> propertiesToUpdate = null) where T : IUserDTO;

      Task DeleteUserAsync(Guid id);
   }
}
