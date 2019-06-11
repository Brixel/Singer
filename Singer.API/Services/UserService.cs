using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Singer.DTOs;
using Singer.Services.Interfaces;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Singer.Data;
using Singer.Models;
using Singer.Helpers.Exceptions;
using Singer.Data.Models;

namespace Singer.Services
{
   public class UserService : IUserService
   {
      private ApplicationDbContext _appContext;
      private readonly IMapper _mapper;
      public UserService(ApplicationDbContext appContext, IMapper mapper)
      {
         _appContext = appContext;
         _mapper = mapper;
      }

      public async Task<TReturn> CreateUserAsync<TCreate, TReturn>(TCreate createUser)
         where TCreate : CreateUserDTO
         where TReturn : IUserDTO, TCreate
      {
         User newUser = _mapper.Map<User>(createUser);
         _appContext.Users.Add(newUser);
         await _appContext.SaveChangesAsync();
         TReturn returnUser = _mapper.Map<TReturn>(newUser);

         return returnUser;
      }

      public async Task<IList<T>> GetAllUsersAsync<T>() where T : IUserDTO
      {
         List<T> users = await Task.FromResult(
            _appContext.Users
               .AsQueryable()
               .ProjectTo<T>(_mapper.ConfigurationProvider)
               .ToList()
         );
         return users;
      }

      public async Task<T> GetUserAsync<T>(Guid id) where T : IUserDTO
      {
         var user = await _appContext.Users.FindAsync(id);
         if (user == null)
         {
            throw new UserNotFoundException();
         }

         var userDTO = _mapper.Map<T>(user);
         return userDTO;
      }

      public async Task<T> UpdateUserAsync<T>(T user, Guid id, IList<string> propertiesToUpdate = null) where T : IUserDTO
      {
         throw new NotImplementedException();
      }
      public async Task DeleteUserAsync(string id)
      {
         throw new NotImplementedException();
      }

      public Task<PaginationModel<T>> GetUsersAsync<T>(int page = 0, Filter<T> filter = null, Sorter<T> sorter = null) where T : IUserDTO
      {
         throw new NotImplementedException();
      }

      public Task DeleteUserAsync(Guid id)
      {
         throw new NotImplementedException();
      }
   }
}
