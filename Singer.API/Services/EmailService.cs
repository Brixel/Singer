using System;
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
      private readonly SmtpClient _smtp;
      private readonly string _mailFrom;

      public EmailService(IOptions<EmailOptions> options = null)
      {
         if(options == null)
         {
            throw new ArgumentNullException(nameof(options));
         }
         var options1 = options.Value;
         _smtp = new SmtpClient
         {
            Credentials = new NetworkCredential(options1.SmtpUsername, options1.SmtpPassword),
            Host = options1.SmtpHost,
            Port = options1.SmtpPort
         };
         _mailFrom = options1.MailFrom;
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
