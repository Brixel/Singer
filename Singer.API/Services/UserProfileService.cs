using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using Singer.Data;
using Singer.Data.Models.Configuration;
using Singer.DTOs.Users;
using Singer.Helpers.Exceptions;
using Singer.Models.Users;
using Singer.Services.Interfaces;

namespace Singer.Controllers;

public class UserProfileService : IUserProfileService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IEmailService<UserDTO> _emailService;
    private readonly string _frontendUrl;

    public UserProfileService(ApplicationDbContext context, UserManager<User> userManager,
       IEmailService<UserDTO> emailService, IOptions<ApplicationConfig> applicationConfigurationOptions)
    {
        _context = context;
        _userManager = userManager;
        _emailService = emailService;
        if (applicationConfigurationOptions == null)
        {
            throw new ArgumentNullException(nameof(applicationConfigurationOptions));
        }
        var applicationConfiguration = applicationConfigurationOptions.Value;
        _frontendUrl = applicationConfiguration.FrontendURL;
    }

    public async Task UpdatePassword(Guid userId, string rawToken, string password)
    {
        var token = rawToken.Replace(" ", "+");
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new UserNotFoundException("No user found");
        }

        var identityResult = await _userManager.ResetPasswordAsync(user, token, password);
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
            throw new UserNotFoundException("No user found");
        }

        var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        var userDTO = new UserDTO()
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName
        };

        var passwordResetURL = $"{_frontendUrl}/auth/reset?userId={user.Id}&token={passwordResetToken}";
        if (_emailService != null)
        {
            await _emailService.SendPasswordResetLink(userDTO, passwordResetURL);
        }

        await _context.SaveChangesAsync();
    }
}
