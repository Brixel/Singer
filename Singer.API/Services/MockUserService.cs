using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Singer.DTOs;
using Singer.Helpers.Extensions;
using Singer.Services.Interfaces;

namespace Singer.Services
{
   public class MockUserService : IUserService
   {
      #region FIELDS

      private readonly IList<IUserDTO> _mockData = new List<IUserDTO>
      {
         new CareUserDTO
         {
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
         new CareUserDTO
         {
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
         new CareUserDTO
         {
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

      public async Task<TReturn> CreateUserAsync<TCreate, TReturn>(TCreate createUser)
         where TCreate : CreateUserDTO
         where TReturn : IUserDTO, TCreate
      {
         // create new return value
         var returnUser = Activator.CreateInstance<TReturn>();

         // copy the properties from the given user to the new user
         foreach (var prop in typeof(TCreate).GetProperties())
            prop.SetValue(returnUser, prop.GetValue(createUser));

         // generate new id
         returnUser.Id = Guid.NewGuid();
         // add the new care user
         _mockData.Add(returnUser);
         // return the new created care user
         return await Task.FromResult(returnUser);
      }

      public async Task<IList<T>> GetAllUsersAsync<T>() where T : IUserDTO
      {
         // return all the mock data
         return await Task.FromResult(
            _mockData
               .Where(x => x.GetType() == typeof(T))
               .Cast<T>()
               .ToList()
         );
      }

      public async Task<IList<T>> GetUsersAsync<T>(
         int start = 0,
         int numberOfElements = 15,
         Filter<T> filter = null,
         Sorter<T> sorter = null) where T : IUserDTO
      {
         var elements = _mockData
            .AsQueryable()
            .OfType<T>();

         if (filter != null)
            elements = elements
               .Where(x => filter.CheckAnd(x));

         
         if (sorter != null && sorter.Count >= 1)
         {
            var sortProperties = sorter.ToList();
            var prop = sortProperties.First();
            var orderedElements = elements.OrderBy(prop);

            if (sorter.Count <= 1)
               return orderedElements.ToList();

            for (var i = 1; i < sorter.Count; i++)
               orderedElements.ThenBy(sortProperties[i]);

            elements = orderedElements;
         }

         return await elements
               .Skip(start)
               .Take(numberOfElements)
               .ToListAsync();
      }

      public async Task<T> GetUserAsync<T>(Guid id) where T : IUserDTO
      {
         // return the care user with the given id
         return await Task.FromResult((T) _mockData.Single(x => x.GetType() == typeof(T) && x.Id == id));
      }

      public async Task<bool> UpdateUserAsync<T>(T user, Guid id) where T : IUserDTO
      {
         // get the index of the care user with the given id
         var i = _mockData
            .Where(x => x.GetType() == typeof(T))
            .Select((u, index) => new {User = u, Index = index})
            .Single(x => x.User.Id == id)
            .Index;

         // set the care user's id to the given id
         user.Id = id; // update the complete care user
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
