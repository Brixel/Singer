using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.DTOs.Users;
using Singer.Helpers;
using Singer.Helpers.Exceptions;
using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Models.Users;

namespace Singer.Services
{
   public class CareUserService : UserService<CareUser, CareUserDTO, CreateCareUserDTO>
   {
      protected override DbSet<CareUser> DbSet => Context.CareUsers;

      public CareUserService(ApplicationDbContext appContext, IMapper mapper, UserManager<User> userManager)
      : base(appContext, mapper, userManager)
      {
      }

      public async Task<TOut> CreateUserAsync<TOut, TIn>(TIn createUser)
         where TIn : CreateCareUserDTO
         where TOut : CareUserDTO
      {
         var user = new User()
         {
            FirstName = createUser.FirstName,
            LastName = createUser.LastName,
            UserName = Guid.NewGuid().ToString()
         };
         var careUser = new CareUser()
         {
            User = user,
            AgeGroup = createUser.AgeGroup,
            BirthDay = createUser.BirthDay,
            CaseNumber = createUser.CaseNumber,
            HasNormalDayCare = createUser.HasNormalDayCare,
            HasResources = createUser.HasResources,
            HasTrajectory = createUser.HasTrajectory,
            HasVacationDayCare = createUser.HasVacationDayCare,
            IsExtern = createUser.IsExtern
         };
         var userCreationResult = await UserManager.CreateAsync(user);
         if (!userCreationResult.Succeeded)
         {
            throw new Exception($"User can not be created. {userCreationResult.Errors.First().Description}");
         }

         DbSet.Add(careUser);
         Context.SaveChanges();

         return (TOut)new CareUserDTO()
         {
            Id = careUser.Id,
            HasNormalDayCare = careUser.HasNormalDayCare,
            AgeGroup = careUser.AgeGroup,
            HasTrajectory = careUser.HasTrajectory,
            HasVacationDayCare = careUser.HasVacationDayCare,
            BirthDay = careUser.BirthDay,
            HasResources = careUser.HasResources,
            CaseNumber = careUser.CaseNumber,
            IsExtern = careUser.IsExtern,
            FirstName = user.FirstName,
            LastName = user.LastName
         };
      }

      public async Task<IList<T>> GetAllUsersAsync<T>() where T : CareUser
      {
         return await Context.Users
            .OfType<T>()
            .ToListAsync();
      }

      public async Task<SearchResults<CareUserDTO>> GetUsersAsync<T>(string sortColumn,
         string sortDirection,
         string filter,
         int page = 0,
         int userPerPage = 15) where T : CareUser
      {
         var users = Context.CareUsers.AsQueryable();

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
            FirstName = x.User.FirstName,
            LastName = x.User.LastName,
            AgeGroup = x.AgeGroup,
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
         var user = await Context.Users.FindAsync(id.ToString()) as T;
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
            userToUpdate = Context.CareUsers.Include(x => x.User).Single(u => u.Id == id);
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
         await Context.SaveChangesAsync();
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
            dbUser = Context.Users.Single(u => u.Id == id);
         }
         catch
         {
            throw new BadInputException();
         }

         Context.Users.Remove(dbUser);
         await Context.SaveChangesAsync();
      }

      protected override Expression<Func<CareUser, bool>> Filter(string filter)
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
