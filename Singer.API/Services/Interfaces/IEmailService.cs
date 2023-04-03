using System.Collections.Generic;
using System.Threading.Tasks;

using Singer.DTOs.Users;

namespace Singer.Services.Interfaces;

/// <summary>
/// Interface that describes the methods implemented specifically for the EmailService.
/// </summary>
public interface IEmailService<TUserDTO>
where TUserDTO : IUserDTO
{
    Task SendAccountDetailsAsync(TUserDTO user, string resetPasswordLink);
    Task SendPasswordResetLink(TUserDTO user, string resetPasswordLink);
}

public interface IEmailService
{
    Task Send(string subject, string body, IReadOnlyList<string> recipients, IReadOnlyList<string> ccEmails);
}
