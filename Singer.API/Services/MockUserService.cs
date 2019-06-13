using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            Id = Guid.NewGuid().ToString(),
            Name = "Joske Vermeulen",
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
            Id = Guid.NewGuid().ToString(),
            Name = "Kim Janssens",
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
            Id = Guid.NewGuid().ToString(),
            Name = "Benjamin Vermeulen",
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
         // create new return value
         var returnUser = Activator.CreateInstance<T>();

         // copy the properties from the given user to the new user
         foreach (var prop in typeof(T).GetProperties())
            prop.SetValue(returnUser, prop.GetValue(createUser));

         // generate new id
         returnUser.Id = Guid.NewGuid().ToString();
         // add the new care user
         _mockData.Add(returnUser);
         // return the new created care user
         return await Task.FromResult(returnUser);
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

      public async Task<SearchResults<T>> GetUsersAsync<T>(
         int start = 0,
         int userPerPage = 15,
         StringFilter<T> filter = null,
         Sorter<T> sorter = null) where T : CareUser
      {
         var usersQueryable = _mockData
            .AsQueryable()
            .OfType<T>();

         if (filter != null)
            usersQueryable = usersQueryable.Filter(filter);

         // Default ordering is by name
         var orderedQueryable = usersQueryable.OrderBy(x => x.Name);
         if (sorter != null && sorter.Count >= 1)
         {
            var sortProperties = sorter.ToList();
            var prop = sortProperties.First();
            orderedQueryable = usersQueryable.OrderBy(prop);

            for (var i = 1; i < sorter.Count; i++)
               orderedQueryable = orderedQueryable.ThenBy(sortProperties[i]);
         }

         var users = await orderedQueryable.TakePage(start, userPerPage).ToListAsync();

         SearchResults<T> result = new SearchResults<T>
         {
            Items = users, Start = start, Size = userPerPage, TotalCount = usersQueryable.Count()
         };

         return result;
      }



      public async Task<T> GetUserAsync<T>(Guid id) where T : CareUser
      {
         // return the care user with the given id
         return await Task.FromResult((T) _mockData.Single(x => x.GetType() == typeof(T) && x.Id == id.ToString()));
      }

      public async Task<bool> UpdateUserAsync<T>(T user, Guid id) where T : CareUser
      {
         // get the index of the care user with the given id
         var i = _mockData
            .Where(x => x.GetType() == typeof(T))
            .Select((u, index) => new {User = u, Index = index})
            .Single(x => x.User.Id == id.ToString())
            .Index;

         // set the care user's id to the given id
         user.Id = id.ToString(); // update the complete care user
         _mockData[i] = user;
         // return the updated care user
         return await Task.FromResult(true);
      }

      public async Task DeleteUserAsync(Guid id)
      {
         // get the index of the care user with the given id
         var i = _mockData
            .Select((user, index) => new {User = user, Index = index})
            .Single(x => x.User.Id == id.ToString())
            .Index;

         // remove the care user from the mock list
         _mockData.RemoveAt(i);

         await Task.CompletedTask;
      }

      #endregion METHODS
   }
}
