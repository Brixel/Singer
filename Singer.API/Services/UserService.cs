using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Singer.Data;
using Singer.DTOs.Users;
using Singer.Helpers.Exceptions;
using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Models.Users;

namespace Singer.Services
{
   public abstract class UserService<TUserEntity, TUserDTO, TCreateUserDTO, TUpdateUserDTO> : DatabaseService<TUserEntity, TUserDTO, TCreateUserDTO, TUpdateUserDTO>
      where TUserEntity : class, IUser
      where TUserDTO : class, IUserDTO
      where TCreateUserDTO : class, ICreateUserDTO
      where TUpdateUserDTO : class, IUpdateUserDTO
   {
      protected UserManager<User> UserManager { get; }
      protected UserService(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager) : base(context, mapper)
      {
         UserManager = userManager;
      }

      public override async Task<TUserDTO> CreateAsync(
         TCreateUserDTO dto)
      {
         // We need usernames for AspNetUsers. However, careusers don't have an e-mail address, so no username can be generated
         // For that reason, we generate a random username.
         if (dto.GetType() == typeof(CreateCareUserDTO) && string.IsNullOrEmpty(dto.Email))
         {
            dto.Email = GenerateRandomUserName(dto.FirstName, dto.LastName);
         }
         else
         {
            var existingEmail = await Queryable.SingleOrDefaultAsync(x => x.User.Email == dto.Email);

            if (existingEmail != null)
            {
               throw new BadInputException("Het email adres dat je opgaf bestaat reeds in de database");
            }
         }

         var baseUser = new User()
         {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            UserName = dto.Email
         };

         // TODO Replace by better temporary password generation approach
         var userCreationResult = await UserManager.CreateAsync(baseUser, "Testpassword123!");
         if (!userCreationResult.Succeeded)
         {
            Debug.WriteLine($"User can not be created. {userCreationResult.Errors.First().Code}");
            throw new InternalServerException($"User can not be created. {userCreationResult.Errors.First().Description}");
         }

         var createdUser = await UserManager.FindByEmailAsync(dto.Email);
         var entity = Mapper.Map<TUserEntity>(dto);
         entity.User = createdUser;
         entity.UserId = createdUser.Id;
         var changeTracker = await Context.AddAsync(entity);
         await Context.SaveChangesAsync();
         return Mapper.Map<TUserDTO>(changeTracker.Entity);
      }

      private string GenerateRandomUserName(string firstName, string lastName)
      {
         var randomGuid = Guid.NewGuid();
         return $"{firstName}.{lastName}.{randomGuid}@test.com";
      }

      /// <summary>
      /// Returns one <see cref="TUserEntity"/> from the database, converted to a <see cref="TUserDTO"/>.
      /// </summary>
      /// <param name="id">The Id OR UserId of the <see cref="TUserEntity"/> to get.</param>
      /// <returns>The <see cref="TUserEntity"/> in the database with Id or UserId <paramref name="id"/> converted to a <see cref="TUserDTO"/>.</returns>
      /// <exception cref="NotFoundException">There is no element found with the id <paramref name="id"/>.</exception>
      public override async Task<TUserDTO> GetOneAsync(Guid id)
      {
         var item = await Queryable.SingleOrDefaultAsync(x => x.Id == id || x.UserId == id);
         if (item == null)
            throw new NotFoundException();

         // return the found entity projected to it's DTO
         return Mapper.Map<TUserDTO>(item);
      }

      public override async Task DeleteAsync(Guid id)
      {
         // search for the entity to delete
         var itemToDelete = await DbSet.FindAsync(id);
         if (itemToDelete == null)
            throw new NotFoundException();

         //Since users are entities based on the `User` entity, we must delete the User instead of the specific entity
         var userToDelete = await Context.Users.FindAsync(itemToDelete.UserId);
         Context.Users.Remove(userToDelete);
         await Context.SaveChangesAsync();
      }

      public override async Task<SearchResults<TUserDTO>> GetAsync(
         string filter = null,
         Expression<Func<TUserDTO, object>> orderer = null,
         ListSortDirection sortDirection = ListSortDirection.Ascending,
         int pageIndex = 0,
         int entitiesPerPage = 15)
      {
         // return the paged results
         return await Queryable
            .ToPagedListAsync(
               Mapper,
               filterExpression: Filter(filter),
               orderByLambda: orderer,
               sortDirection: sortDirection,
               pageIndex: pageIndex,
               pageSize: entitiesPerPage);
      }

      public override async Task<TUserDTO> UpdateAsync(Guid id, TUpdateUserDTO dto)
      {
         var existingEmail = await Queryable.SingleOrDefaultAsync(x =>
            x.User.Email != null &&
            x.User.Email == dto.Email &&
            x.Id != id);

         if (existingEmail != null)
         {
            throw new BadInputException("Het email adres dat je opgaf bestaat reeds in de database");
         }

         return await base.UpdateAsync(id, dto);
      }

   }

}
