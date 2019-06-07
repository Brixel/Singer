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

      public async Task<T> CreateUserAsync<T>(T user) where T : UserDTO
      {
         if (user.Id != null)
         {
            throw new BadInputException("Id cannot be passed when creating new user!");
         }

         User newUser = _mapper.Map<User>(user);
         _appContext.Users.Add(newUser);
         await _appContext.SaveChangesAsync();
         T returnUser = _mapper.Map<T>(newUser);

         return returnUser;
      }

      public async Task<IList<T>> GetAllUsersAsync<T>() where T : UserDTO
      {
         List<T> users = await Task.FromResult(
            _appContext.Users
               .AsQueryable()
               .ProjectTo<T>(_mapper.ConfigurationProvider)
               .ToList()
         );
         return users;
      }

      public async Task<T> GetUserAsync<T>(Guid id) where T : UserDTO
      {
         var user = await _appContext.Users.FindAsync(id);
         if (user == null)
         {
            throw new UserNotFoundException();
         }

         var userDTO = _mapper.Map<T>(user);
         return userDTO;
      }

      public async Task<T> UpdateUserAsync<T>(T user, Guid id, IList<string> propertiesToUpdate = null) where T : UserDTO
      {
         throw new NotImplementedException();
      }
      public async Task DeleteUserAsync(string id)
      {
         throw new NotImplementedException();
      }

      public Task<PaginationModel<T>> GetUsersAsync<T>(int page = 0, Filter<T> filter = null, string sortPropertyName = null) where T : UserDTO
      {
         throw new NotImplementedException();
      }

      public Task DeleteUserAsync(Guid id)
      {
         throw new NotImplementedException();
      }
   }
}
