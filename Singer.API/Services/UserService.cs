using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Singer.Data;
using Singer.DTOs.Users;
using Singer.Helpers.Exceptions;
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

         var userCreationResult = await UserManager.CreateAsync(baseUser);
         if (!userCreationResult.Succeeded)
         {
            Debug.WriteLine($"User can not be created. {userCreationResult.Errors.First().Code}");
            throw new InternalServerException($"User can not be created. {userCreationResult.Errors.First().Description}");

         }

         return await base.CreateAsync(dto, dtoToEntityProjector, entityToDTOProjector);
      }
   }

}
