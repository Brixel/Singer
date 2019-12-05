using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Singer.Data.Models.Configuration;
using Singer.DTOs.Users;
using Singer.Services.Interfaces;

namespace Singer.Services
{
   public class EmailService<TUserDTO> : IEmailService<TUserDTO>
   where TUserDTO : IUserDTO
   {
      private SmtpClient _smtp;
      private string _mailFrom;
      private readonly EmailOptions _options;
      public EmailService(IOptions<EmailOptions> options = null)
      {
         _options = options.Value;
         _smtp = new SmtpClient
         {
            Credentials = new NetworkCredential(_options.SmtpUsername, _options.SmtpPassword),
            Host = _options.SmtpHost,
            Port = _options.SmtpPort
         };
         _mailFrom = _options.MailFrom;
      }
      public async Task SendAccountDetailsAsync(TUserDTO user, string resetPasswordLink)
      {
         var msg = new MailMessage(_mailFrom, user.Email);
         msg.Subject = "Uw Sint-Gerardus account gegevens";
         msg.Body = "Hierunder vind u uw gebruikersnaam voor Sint-Gerardus, en een link om uw wachtwoord de eerste in te stellen:\n";
         msg.Body += $"Gebruikersnaam: {user.Email}\n";
         msg.Body += $"Klik hier om uw wachtwoord in te stellen: {resetPasswordLink}";
         await _smtp.SendMailAsync(msg);
      }
   }
}
