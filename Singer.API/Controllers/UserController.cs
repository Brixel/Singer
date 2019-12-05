using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Singer.Models.Users;
using Singer.Services;

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
       public async Task<bool> UpdatePassword([FromBody] UpdatePasswordDTO updatePassword)
       {
          return await _userProfileService.UpdatePassword(updatePassword.UserId, updatePassword.Token, updatePassword.NewPassword);
       }
    }

    public class UpdatePasswordDTO
    {
       public Guid UserId { get; set; }
       public string Token { get; set; }

       public string NewPassword { get; set; }
    }

    public interface IUserProfileService
    {
       Task<bool> UpdatePassword(Guid userId, string token, string password);
    }

    public class UserProfileService : IUserProfileService
    {
       private readonly UserManager<User> _userManager;

       public UserProfileService(UserManager<User> userManager)
       {
          _userManager = userManager;
       }

       public async Task<bool> UpdatePassword(Guid userId, string token, string password)
       {
          var user = await _userManager.FindByIdAsync(userId.ToString());
          if (user == null)
          {
             throw new Exception("No user found");
          }

          var identityResult = await _userManager.ResetPasswordAsync(user, token, password);
          return identityResult.Succeeded;
       }
   }
}
