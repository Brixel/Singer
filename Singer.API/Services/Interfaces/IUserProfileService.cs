using System;
using System.Threading.Tasks;

namespace Singer.Services.Interfaces;

public interface IUserProfileService
{
    Task UpdatePassword(Guid userId, string rawToken, string password);
    Task RequestPasswordReset(string userId);
}
