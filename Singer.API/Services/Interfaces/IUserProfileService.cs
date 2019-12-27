using System;
using System.Threading.Tasks;

namespace Singer.Controllers
{
   public interface IUserProfileService
   {
      Task UpdatePassword(Guid userId, string rawToken, string password);
      Task RequestPasswordReset(string userId);
   }
}
