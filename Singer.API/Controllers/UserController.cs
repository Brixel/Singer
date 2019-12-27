using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Singer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       private readonly IUserProfileService _userProfileService;

       public UserController(IUserProfileService userProfileService)
       {
          _userProfileService = userProfileService;
       }
       [HttpPut("password")]
       public async Task UpdatePassword([FromBody] UpdatePasswordDTO updatePassword)
       {
          await _userProfileService.UpdatePassword(updatePassword.UserId, updatePassword.Token, updatePassword.NewPassword);
       }

       [HttpPost("resetpassword")]
       public async Task RequestPasswordReset([FromBody] string userId)
       {
          await _userProfileService.RequestPasswordReset(userId);
       }
    }
}
