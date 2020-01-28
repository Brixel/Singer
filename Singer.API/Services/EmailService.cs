using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Singer.Data.Models.Configuration;
using Singer.DTOs.Users;
using Singer.Services.Interfaces;

namespace Singer.Services
{
   public class NoActualEmailService<T> : IEmailService<T>
      where T : IUserDTO
   {
      public Task SendAccountDetailsAsync(T user, string resetPasswordLink)
      {
         return Task.CompletedTask;
      }

      public Task SendPasswordResetLink(T user, string resetPasswordLink)
      {
         return Task.CompletedTask;
      }
   }

   public class NoActualEmailService : IEmailService
   {
      public Task Send(string subject, string body, IReadOnlyList<string> recipients)
      {
         return Task.CompletedTask;
      }
   }
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
         var emailOptions = options.Value;
         _smtp = new SmtpClient
         {
            Credentials = new NetworkCredential(emailOptions.SmtpUsername, emailOptions.SmtpPassword),
            Host = emailOptions.SmtpHost,
            Port = emailOptions.SmtpPort
         };
         _mailFrom = emailOptions.MailFrom;
      }
      public async Task SendAccountDetailsAsync(TUserDTO user, string resetPasswordLink)
      {
         var msg = new MailMessage(_mailFrom, user.Email);
         msg.IsBodyHtml = true;
         msg.Subject = "Uw Sint-Gerardus account gegevens";
         msg.Body = "Hieronder vind u uw gebruikersnaam voor Sint-Gerardus, en een link om uw wachtwoord de eerste in te stellen:<br />";
         msg.Body += $"Gebruikersnaam: {user.Email}<br />";
         msg.Body += $"Klik hier om uw wachtwoord in te stellen: <a href=\"{resetPasswordLink}\">Herstel wachtwoord</a><br />";
         await _smtp.SendMailAsync(msg);
      }

      public async Task SendPasswordResetLink(TUserDTO user, string resetPasswordLink)
      {
         var msg = new MailMessage(_mailFrom, user.Email);
         msg.IsBodyHtml = true;
         msg.Subject = "Herstel uw Sint Gerardus wachtwoord";
         msg.Body = $"Beste {user.FirstName} {user.LastName},\n Klik op onderstaande link om uw wachtwoord te wijzigen:<br />";
         msg.Body += $"<a href=\"{resetPasswordLink}\">Herstel wachtwoord</a>";
         await _smtp.SendMailAsync(msg);
      }
   }


   public class EmailService : IEmailService
   {
      private readonly string _mailFrom;
      private readonly SmtpClient _smtp;

      public EmailService(IOptions<EmailOptions> options = null)
      {

         if (options == null)
         {
            throw new ArgumentNullException(nameof(options));
         }
         var emailOptions = options.Value;
         _smtp = new SmtpClient
         {
            Credentials = new NetworkCredential(emailOptions.SmtpUsername, emailOptions.SmtpPassword),
            Host = emailOptions.SmtpHost,
            Port = emailOptions.SmtpPort
         };
         _mailFrom = emailOptions.MailFrom;
      }
      public async Task Send(string subject, string body, IReadOnlyList<string> recipients)
      {
         var msg = new MailMessage
         {
            From = new MailAddress(_mailFrom),
            IsBodyHtml = true,
            Subject = subject,
            Body = body
         };
         foreach (var recipient in recipients)
         {
            msg.To.Add(new MailAddress(recipient));
         }
         await _smtp.SendMailAsync(msg);
      }
   }
}
