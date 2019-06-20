using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Singer.DTOs;
using Singer.Models;

namespace Singer.Services.Interfaces
{
   public interface IUserService
   {
      Task<TOut> CreateUserAsync<TOut, TIn>(TIn createUser)
         where TIn : CreateCareUserDTO
         where TOut : CareUserDTO;

      Task<IList<T>> GetAllUsersAsync<T>() where T : CareUser;

      Task<SearchResults<CareUserDTO>> GetUsersAsync<T>(string sortColumn,
         string sortDirection,
         string filter,
         int page = 0,
         int userPerPage = 15)
         where T : CareUser;

      Task<T> GetUserAsync<T>(Guid id) where T : CareUser;

      Task<CareUserDTO> UpdateUserAsync(CreateCareUserDTO user, Guid id);

      Task DeleteUserAsync(Guid id);
   }
}
