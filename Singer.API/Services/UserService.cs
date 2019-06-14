using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Singer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.Models;
using Singer.Helpers.Exceptions;
using Singer.Helpers.Extensions;

namespace Singer.Services
{
   public class UserService : IUserService
   {
      private ApplicationDbContext _appContext;

      public UserService(ApplicationDbContext appContext)
      {
         _appContext = appContext;
      }

      public async Task<T> CreateUserAsync<T>(T createUser) where T : CareUser
      {
         // TODO Use Usermanager
         return null;
         
      }

      public async Task<IList<T>> GetAllUsersAsync<T>() where T : CareUser
      {
         return await _appContext.Users
            .OfType<T>()
            .ToListAsync();
      }

      public async Task<SearchResults<T>> GetUsersAsync<T>(
         int start = 0,
         int userPerPage = 15,
         StringFilter<T> filter = null,
         Sorter<T> sorter = null) where T : CareUser
      {
         SearchResults<T> result = new SearchResults<T>();
         var users = _appContext.Users
            .OfType<T>()
            .Filter(filter);

         result.TotalCount = users.Count();
                          
         result.Items = await users
            .Skip(start)
            .Take(userPerPage)
            .ToListAsync();

         result.Start = start;
         result.Size = userPerPage;

         return result;
      }

      public async Task<T> GetUserAsync<T>(Guid id) where T : CareUser
      {
         var user = await _appContext.Users.FindAsync(id.ToString()) as T;
         if (user == null)
         {
            throw new UserNotFoundException();
         }

         return user;
      }

      public async Task<bool> UpdateUserAsync<T>(T user, Guid id) where T : CareUser
      {
         User userToUpdate;
         try
         {
            //Check if id exists
            userToUpdate = _appContext.Users.Single(u => u.Id == id);
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
         userToUpdate = user.User;
         //userToUpdate.Id = id.ToString();

         //And finally update database
         _appContext.Users.Update(userToUpdate);
         await _appContext.SaveChangesAsync();
         return true;
      }

      public async Task DeleteUserAsync(Guid id)
      {
         User dbUser;
         try
         {
            //Check if id exists
            dbUser = _appContext.Users.Single(u => u.Id == id);
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
