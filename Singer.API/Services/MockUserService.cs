using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Singer.DTOs;
using Singer.Helpers;
using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Services.Interfaces;

namespace Singer.Services
{
   public class MockUserService : IUserService
   {
      #region FIELDS

      private readonly IList<CareUser> _mockData = new List<CareUser>
      {
         new CareUser
         {
            Id = Guid.NewGuid(),
            User = new User() {
               LastName = "Joske Vermeulen"

            },
            BirthDay = DateTime.Parse("06/07/2008", CultureInfo.InvariantCulture),
            CaseNumber = "0123456789",
            AgeGroup = Models.AgeGroup.Child,
            IsExtern = false,
            HasTrajectory = true,
            HasNormalDayCare = true,
            HasVacationDayCare = true,
            HasResources = true
         },
         new CareUser
         {
            Id = Guid.NewGuid(),
            User = new User(){
            LastName = "Kim Janssens"},
            BirthDay = DateTime.Parse("08/07/2006", CultureInfo.InvariantCulture),
            CaseNumber = "9876543210",
            AgeGroup = Models.AgeGroup.Child,
            IsExtern = true,
            HasTrajectory = true,
            HasNormalDayCare = true,
            HasVacationDayCare = true,
            HasResources = true
         },
         new CareUser
         {
            Id = Guid.NewGuid(),
            User = new User()
            {LastName = "Benjamin Vermeulen"
            }
,
            BirthDay = DateTime.Parse("06/08/2010", CultureInfo.InvariantCulture),
            CaseNumber = "091837465",
            AgeGroup = Models.AgeGroup.Youngster,
            IsExtern = false,
            HasTrajectory = true,
            HasNormalDayCare = true,
            HasVacationDayCare = true,
            HasResources = false
         },
      };

      #endregion FIELDS


      #region CONSTRUCTOR

      #endregion CONSTRUCTOR


      #region METHODS

      public async Task<T> CreateUserAsync<T>(T createUser) where T : CareUser
      {
         // TODO User UserManager
         throw new NotImplementedException();
      }

      public async Task<IList<T>> GetAllUsersAsync<T>() where T : CareUser
      {
         // return all the mock data
         return await Task.FromResult(
            _mockData
               .Where(x => x.GetType() == typeof(T))
               .Cast<T>()
               .ToList()
         );
      }

      public async Task<SearchResults<CareUserDTO>> GetUsersAsync<T>(string sortColumn,
         string sortDirection,
         string filter,
         int page = 0,
         int userPerPage = 15) where T : CareUser
      {
         var usersQueryable = _mockData
            .AsQueryable()
            .OfType<T>();

         var orderByLambda = PropertyHelpers.GetPropertySelector<CareUserDTO>(sortColumn);
         var result = usersQueryable.ToPagedList(Filter(filter), ProjectToCareUserDTO(),
            orderByLambda, sortDirection, page,
            userPerPage);

         return result;
      }
      private static Expression<Func<CareUser, bool>> Filter(string filter)
      {
         Expression<Func<CareUser, bool>> filterExpression = f => f.User.LastName.Contains(filter);
         return filterExpression;
      }

      private static Expression<Func<CareUser, CareUserDTO>> ProjectToCareUserDTO()
      {
         return x => new CareUserDTO
         {
            Id = x.Id,
            UserId = x.UserId,
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
         // return the care user with the given id
         return await Task.FromResult((T) _mockData.Single(x => x.GetType() == typeof(T) && x.Id == id));
      }

      public async Task<bool> UpdateUserAsync<T>(T user, Guid id) where T : CareUser
      {
         // get the index of the care user with the given id
         var i = _mockData
            .Where(x => x.GetType() == typeof(T))
            .Select((u, index) => new {User = u, Index = index})
            .Single(x => x.User.Id == id)
            .Index;

         // set the care user's id to the given id
         //user.Id = id.ToString(); // update the complete care user
         _mockData[i] = user;
         // return the updated care user
         return await Task.FromResult(true);
      }

      public async Task DeleteUserAsync(Guid id)
      {
         // get the index of the care user with the given id
         var i = _mockData
            .Select((user, index) => new {User = user, Index = index})
            .Single(x => x.User.Id == id)
            .Index;

         // remove the care user from the mock list
         _mockData.RemoveAt(i);

         await Task.CompletedTask;
      }

      #endregion METHODS
   }

}
