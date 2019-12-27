using System;
using System.Threading.Tasks;

namespace Singer.Controllers
{
   public interface IUserProfileService
   {
      Task UpdatePassword(Guid userId, string token, string password);
      Task RequestPasswordReset(string userId);
   }
}