using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Singer.Data;
using Singer.Data.Models.Configuration;
using Singer.DTOs.Users;
using Singer.Helpers.Exceptions;
using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Models.Users;
using Singer.Resources;
using Singer.Services.Interfaces;

namespace Singer.Services
{
   public abstract class UserService<TUserEntity, TUserDTO, TCreateUserDTO, TUpdateUserDTO> : DatabaseService<TUserEntity, TUserDTO, TCreateUserDTO, TUpdateUserDTO>
      where TUserEntity : class, IUser
      where TUserDTO : class, IUserDTO
      where TCreateUserDTO : class, ICreateUserDTO
      where TUpdateUserDTO : class, IUpdateUserDTO
   {
      protected UserManager<User> UserManager { get; }
      private readonly IEmailService<TUserDTO> _emailService;
      private readonly string _frontendURL;

      protected UserService(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager,
        IEmailService<TUserDTO> emailService, IOptions<ApplicationConfig> applicationConfigurationOptions) : base(context, mapper)
      {
         UserManager = userManager;
         _emailService = emailService;
         if (applicationConfigurationOptions == null)
         {
            throw new ArgumentNullException(nameof(applicationConfigurationOptions));
         }

         _frontendURL = applicationConfigurationOptions.Value.FrontendURL;
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
               throw new BadInputException("The email address must be unique to each user.", ErrorMessages.DuplicateEmail);
         }

         var baseUser = new User()
         {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            UserName = dto.Email
         };

         var userCreationResult = await UserManager.CreateAsync(baseUser);
         var passwordResetToken = await UserManager.GeneratePasswordResetTokenAsync(baseUser);
         Console.WriteLine($"Password reset token: {passwordResetToken}");
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
         var userDTO = Mapper.Map<TUserDTO>(changeTracker.Entity);
         var passwordResetURL = $"{_frontendURL}/auth/reset?userId={createdUser.Id}&token={passwordResetToken}";
         if (_emailService != null)
         {
            await _emailService.SendAccountDetailsAsync(userDTO, passwordResetURL);
         }
         return userDTO;
      }

      private string GenerateRandomUserName(string firstName, string lastName)
      {
         var randomGuid = Guid.NewGuid();
         return $"{firstName}.{lastName}.{randomGuid}@test.com";
      }

      public override async Task<TUserDTO> GetOneAsync(Guid id)
      {
         // No async usage due to Automapper not being able to process IAsyncEnumerables
         var item = await Queryable
            .ProjectTo<TUserDTO>(Mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.Id == id);
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
            throw new BadInputException("The email addres must be unique to each user.", ErrorMessages.DuplicateEmail);

         return await base.UpdateAsync(id, dto);
      }

   }
}
