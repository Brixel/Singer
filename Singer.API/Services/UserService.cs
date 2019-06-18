using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.DTOs;
using Singer.Helpers;
using Singer.Helpers.Exceptions;
using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Services.Interfaces;

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
         throw new NotImplementedException();
      }

      public async Task<IList<T>> GetAllUsersAsync<T>() where T : CareUser
      {
         return await _appContext.Users
            .OfType<T>()
            .ToListAsync();
      }

      public async Task<SearchResults<CareUserDTO>> GetUsersAsync<T>(string sortColumn,
         string sortDirection,
         string filter,
         int page = 0,
         int userPerPage = 15) where T : CareUser
      {
         var users = _appContext.CareUsers.AsQueryable();

         var orderByLambda = PropertyHelpers.GetPropertySelector<CareUserDTO>(sortColumn);

         var result = users.ToPagedList(Filter(filter), ProjectToCareUserDTO(),
            orderByLambda, sortDirection, page,
            userPerPage);

         return result;
      }

      private static Expression<Func<CareUser, CareUserDTO>> ProjectToCareUserDTO()
      {
         return x => new CareUserDTO
         {
            Id = x.Id,
            UserId = x.UserId,
            FirstName = x.User.FirstName,
            LastName = x.User.LastName,
            AgeGroup = x.AgeGroup,
            UserName = x.User.UserName,
            HasTrajectory = x.HasTrajectory,
            HasVacationDayCare = x.HasVacationDayCare,
            BirthDay = x.BirthDay,
            HasNormalDayCare = x.HasNormalDayCare,
            HasResources = x.HasResources,
            CaseNumber = x.CaseNumber,
            IsExtern = x.IsExtern,
            Email = x.User.Email

         };
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

      public async Task<CareUserDTO> UpdateUserAsync(CreateCareUserDTO user, Guid id)
      {
         CareUser userToUpdate;
         try
         {
            //Check if id exists
            userToUpdate = _appContext.CareUsers.Include(x => x.User).Single(u => u.Id == id);
         }
         catch
         {
            throw new BadInputException();
         }

         //Convert user DTO to view
         userToUpdate.AgeGroup = user.AgeGroup;
         userToUpdate.User.FirstName = user.FirstName;
         userToUpdate.User.LastName = user.LastName;
         userToUpdate.BirthDay = user.BirthDay;
         userToUpdate.CaseNumber = user.CaseNumber;
         userToUpdate.HasNormalDayCare = user.HasNormalDayCare;
         userToUpdate.HasResources = user.HasResources;
         userToUpdate.HasTrajectory = user.HasTrajectory;
         userToUpdate.HasVacationDayCare = user.HasVacationDayCare;
         userToUpdate.IsExtern = user.IsExtern;

         //And finally update database
         await _appContext.SaveChangesAsync();
         return new CareUserDTO()
         {
            Id = userToUpdate.Id,
            AgeGroup = user.AgeGroup,
            FirstName = user.FirstName,
            LastName = user.LastName,
            BirthDay = user.BirthDay,
            CaseNumber = user.CaseNumber,
            HasNormalDayCare = user.HasNormalDayCare,
            HasResources = user.HasResources,
            HasTrajectory = user.HasTrajectory,
            HasVacationDayCare = user.HasVacationDayCare,
            IsExtern = user.IsExtern
         };
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

      private static Expression<Func<CareUser, bool>> Filter(string filter)
      {
         if (string.IsNullOrWhiteSpace(filter))
         {
            return o => true;
         }
         Expression<Func<CareUser, bool>> filterExpression =
            f =>
               f.User.FirstName.Contains(filter) ||
               f.User.LastName.Contains(filter) ||
               f.CaseNumber.Contains(filter);
         return filterExpression;
      }
   }
}
