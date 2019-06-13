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

      public async Task<SearchResults<T>> GetUsersAsync<T>(
         int start = 0,
         int numberOfElements = 15,
         StringFilter<T> filter = null,
         Sorter<T> sorter = null) where T : IUserDTO
      {
         List<T> users = await Task.FromResult(
            _appContext.Users
               .Skip(start)
               .Take(numberOfElements)
               .AsQueryable()
               .ProjectTo<T>(_mapper.ConfigurationProvider)
               .ToList()
         );

         SearchResults<T> result = new SearchResults<T>();
         result.Items = users;
         result.Start = start;
         result.Size = numberOfElements;
         result.TotalCount = _appContext.Users.Count();
         return result;
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

      public async Task<bool> UpdateUserAsync<T>(T user, Guid id) where T : IUserDTO
      {
         User dbUser;
         try
         {
            //Check if id exists
            dbUser = _appContext.Users.Single(u => u.Id == id.ToString());
         }
         catch
         {
            throw new BadInputException();
         }

         //Ensure client is not trying to change the ID
         if (user.Id != id)
         {
            throw new BadInputException();
         }

         //Convert user DTO to view
         _mapper.Map(user, dbUser);

         //And finally update database
         _appContext.Users.Update(dbUser);
         await _appContext.SaveChangesAsync();
         return true;
      }
      public async Task DeleteUserAsync(Guid id)
      {
         User dbUser;
         try
         {
            //Check if id exists
            dbUser = _appContext.Users.Single(u => u.Id == id.ToString());
         }
         catch
         {
            throw new BadInputException();
         }

         _appContext.Users.Remove(dbUser);
         await _appContext.SaveChangesAsync();
      }
   }
}
