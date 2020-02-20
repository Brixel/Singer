using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs;
using Singer.DTOs.Users;
using Singer.Helpers.Extensions;
using Singer.Services.Interfaces;

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

       [HttpGet("me")]
       public async Task GetUserInfo()
       {
          var userId = User.GetUserId();
       }
    }
}
