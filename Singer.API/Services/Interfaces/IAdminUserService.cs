using Singer.DTOs.Users;
using Singer.Models.Users;

namespace Singer.Services.Interfaces
{
   public interface IAdminUserService : IDatabaseService<AdminUser, AdminUserDTO, CreateAdminUserDTO, UpdateAdminUserDTO>
   {

   }
}
