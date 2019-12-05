using System.Threading.Tasks;
using Singer.DTOs.Users;

namespace Singer.Services.Interfaces
{
   /// <summary>
   /// Interface that describes the methods implemented specifically for the EmailService.
   /// </summary>
   public interface IEmailService<TUserDTO>
   where TUserDTO : IUserDTO
   {
      Task SendAccountDetailsAsync(TUserDTO userDTO, string resetPasswordLink);
   }
}
