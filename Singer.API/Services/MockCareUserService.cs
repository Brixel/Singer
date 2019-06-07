using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Singer.DTOs;
using Singer.Services.Interfaces;

namespace Singer.Services
{
   public class MockCareUserService : ICareUserService
   {
      #region FIELDS

      private readonly IList<CareUserDTO> _mockData = new List<CareUserDTO>
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

      #region METHODS

      public async Task<CareUserDTO> CreateCareUserAsync(CareUserDTO careUser)
      {
         // generate new id
         careUser.Id = Guid.NewGuid().ToString();
         // add the new care user
         _mockData.Add(careUser);
         // return the new created care user
         return await Task.FromResult(careUser);
      }

      public async Task<IList<CareUserDTO>> GetAllCareUsersAsync()
      {
         // return all the mock data
         return await Task.FromResult(_mockData);
      }

      public async Task<CareUserDTO> GetCareUserAsync(string id)
      {
         // check valid id
         Guid.Parse(id);
         // return the care user with the given id
         return await Task.FromResult(_mockData.Single(x => x.Id == id));
      }

      public async Task<CareUserDTO> UpdateCareUserAsync(
         CareUserDTO careUser,
         string id,
         IList<string> propertiesToUpdate = null)
      {
         // check valid id
         Guid.Parse(id);
         // get the index of the care user with the given id
         var i = _mockData
            .Select((user, index) => new { User = user, Index = index })
            .Single(x => x.User.Id == id)
            .Index;

         // set the care user's id to the given id 
         careUser.Id = id;
         if (propertiesToUpdate == null)
         {
            // update the complete care user
            _mockData[i] = careUser;
            // return the updated care user
            return await Task.FromResult(_mockData[i]);
         }

         // get the properties of the CareUserDTO without the id property
         var props = typeof(CareUserDTO)
            .GetProperties()
            .Where(x => x.Name != nameof(CareUserDTO.Id));

         // update all the properties listed in the properties to update
         foreach (var propertyInfo in props)
         {
            if (propertiesToUpdate.Any(x => x == propertyInfo.Name))
               propertyInfo.SetValue(_mockData[i], propertyInfo.GetValue(careUser));
         }

         // return the new care user
         return await Task.FromResult(_mockData[i]);
      }

      public async Task DeleteCareUserAsync(string id)
      {
         // check valid id
         Guid.Parse(id);
         // get the index of the care user with the given id
         var i = _mockData
            .Select((user, index) => new { User = user, Index = index })
            .Single(x => x.User.Id == id)
            .Index;

         // remove the care user from the mock list
         _mockData.RemoveAt(i);

         await Task.CompletedTask;
      }

      #endregion METHODS
   }
}
