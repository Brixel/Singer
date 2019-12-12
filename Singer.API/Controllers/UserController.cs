using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Singer.Data;
using Singer.DTOs.Users;
using Singer.Helpers.Exceptions;
using Singer.Models.Users;
using Singer.Services;
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
    }

    public class UpdatePasswordDTO
    {
       public Guid UserId { get; set; }
       public string Token { get; set; }

       public string NewPassword { get; set; }
    }

    public interface IUserProfileService
    {
       Task UpdatePassword(Guid userId, string token, string password);
       Task RequestPasswordReset(string userId);
    }

    public class UserProfileService : IUserProfileService
    {
       private readonly ApplicationDbContext _context;
       private readonly UserManager<User> _userManager;
       private readonly IEmailService<UserDTO> _emailService;

       public UserProfileService(ApplicationDbContext context,UserManager<User> userManager, IEmailService<UserDTO> emailService)
       {
          _context = context;
          _userManager = userManager;
          _emailService = emailService;
       }

       public async Task UpdatePassword(Guid userId, string token, string password)
       {
          var code = token.Replace(" ", "+");
         var user = await _userManager.FindByIdAsync(userId.ToString());
          if (user == null)
          {
             throw new Exception("No user found");
          }

          var identityResult = await _userManager.ResetPasswordAsync(user, code, password);
          if (identityResult.Errors.Any())
          {
             throw new BadInputException(identityResult.Errors.First().Code, new Exception(identityResult.Errors.First().Code));
          }
       }

       public async Task RequestPasswordReset(string userId)
       {
         var user = await _userManager.FindByIdAsync(userId);
         if (user == null)
         {
            throw new Exception("No user found");
         }

         var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

         var userDTO = new UserDTO()
         {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName
         };

         var passwordResetURL = $"https://localhost:5001/auth/reset?userId={user.Id}&token={passwordResetToken}";
         if (_emailService != null)
         {
            await _emailService.SendPasswordResetLink(userDTO, passwordResetURL);
         }

         await _context.SaveChangesAsync();
       }
    }
}
