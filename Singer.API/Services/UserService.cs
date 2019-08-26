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
   public abstract class UserService<TUserEntity, TUserDTO, TCreateUserDTO> : DatabaseService<TUserEntity, TUserDTO, TCreateUserDTO>
      where TUserEntity : class, IUser
      where TUserDTO : class, IUserDTO
      where TCreateUserDTO : class, ICreateUserDTO
   {
      protected UserManager<User> UserManager { get; }
      protected UserService(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager) : base(context, mapper)
      {
         UserManager = userManager;
      }

      public override async Task<TUserDTO> CreateAsync(
         TCreateUserDTO dto,
         Expression<Func<TCreateUserDTO, TUserEntity>> dtoToEntityProjector = null,
         Expression<Func<TUserEntity, TUserDTO>> entityToDTOProjector = null)
      {
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
         entity.UserId = createdUser.Id;

         return await base.CreateAsync(dto, _ => entity, entityToDTOProjector);
      }

      public override async Task<TUserDTO> GetOneAsync(Guid id, Expression<Func<TUserEntity, TUserDTO>> projector = null)
      {
         // set the projector if it is null
         if (projector == null)
            projector = EntityToDTOProjector;

         // search for the entity with the given id in the database
         var item = await Queryable //Explicitly load the user entity
            .Select(projector).SingleOrDefaultAsync(x => x.Id == id);
         if (item == null)
            throw new NotFoundException();

         // return the found entity projected to it's DTO
         return item;
      }

      public override async Task DeleteAsync(Guid id)
      {
         // search for the entity to delete
         var itemToDelete = await DbSet.FindAsync(id);
         if (itemToDelete == null)
            throw new NotFoundException();

         //Since users are entities based on the `User` entity, we must delete the User isntead of the specific entity
         var userToDelete = await Context.Users.FindAsync(itemToDelete.UserId);
         Context.Users.Remove(userToDelete);
         await Context.SaveChangesAsync();
      }

      public override async Task<SearchResults<TUserDTO>> GetAsync(
         string filter = null,
         Expression<Func<TUserEntity, TUserDTO>> projector = null,
         Expression<Func<TUserDTO, object>> orderer = null,
         ListSortDirection sortDirection = ListSortDirection.Ascending,
         int pageIndex = 0,
         int entitiesPerPage = 15)
      {
         // set the projector if it is null
         if (projector == null)
            projector = EntityToDTOProjector;

         // return the paged results
         return await DbSet
            .Include(x => x.User)
            .AsQueryable()
            .ToPagedListAsync(
               filterExpression: Filter(filter),
               projectionExpression: projector,
               orderByLambda: orderer,
               sortDirection: sortDirection,
               pageIndex: pageIndex,
               pageSize: entitiesPerPage);
      }

   }

}
