using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Singer.Data.Models;
using Singer.DTOs;
using Singer.Services.Interfaces;
using AutoMapper;

namespace Singer.Services
{
   public class MockUserService : IUserService
   {
      #region FIELDS

      private readonly IMapper _mapper;

      private readonly IList<IUserDTO> _mockData = new List<CareUserDTO>
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
         }
         .Cast<IUserDTO>()
         .ToList();

      #endregion FIELDS


      #region CONSTRUCTOR

      public MockUserService(IMapper mapper)
      {
         _mapper = mapper;
      }

      #endregion CONSTRUCTOR


      #region METHODS

      public async Task<TReturn> CreateUserAsync<TCreate, TReturn>(TCreate careUser)
         where TCreate : CreateUserDTO
         where TReturn : IUserDTO, TCreate
      {
         TReturn returnUser = _mapper.Map<TReturn>(careUser);
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

      public Task<PaginationModel<T>> GetUsersAsync<T>(int page = 0, Filter<T> filter = null, Sorter<T> sorter = null)
         where T : IUserDTO
      {
         throw new NotImplementedException();
      }

      public async Task<T> GetUserAsync<T>(Guid id) where T : IUserDTO
      {
         // return the care user with the given id
         return await Task.FromResult((T) _mockData.Single(x => x.GetType() == typeof(T) && x.Id == id)
         );
      }

      public async Task<T> UpdateUserAsync<T>(
         T user,
         Guid id,
         IList<string> propertiesToUpdate = null) where T : IUserDTO
      {
         // get the index of the care user with the given id
         var i = _mockData
            .Where(x => x.GetType() == typeof(T))
            .Select((u, index) => new {User = u, Index = index})
            .Single(x => x.User.Id == id)
            .Index;

         // set the care user's id to the given id
         user.Id = id;
         if (propertiesToUpdate == null)
         {
            // update the complete care user
            _mockData[i] = user;
            // return the updated care user
            return await Task.FromResult((T) _mockData[i]);
         }

         // get the properties of the CareUserDTO without the id property
         var props = typeof(T)
            .GetProperties()
            .Where(x => x.Name != nameof(UserDTO.Id));

         // update all the properties listed in the properties to update
         foreach (var propertyInfo in props)
         {
            if (propertiesToUpdate.Any(x => x == propertyInfo.Name))
               propertyInfo.SetValue(_mockData[i], propertyInfo.GetValue(user));
         }

         // return the new care user
         return await Task.FromResult((T) _mockData[i]);
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
