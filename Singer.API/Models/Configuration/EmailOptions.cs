using Singer.Helpers.Extensions;

namespace Singer.Data.Models.Configuration
{
   public class EmailOptions
   {
      public const string SECTION_NAME = "EmailOptions";
      public string SmtpHost { get; set; }
      public string SmtpUsername { get; set; }
      public string SmtpPassword { get; set; }
      public int SmtpPort { get; set; }
      public string MailFrom { get; set; }
      public bool IsValid
      {
         get
         {
            return SmtpHost.HasData() && SmtpPort > 0 && MailFrom.HasData()
               && (SmtpUsername.HasData() && SmtpPassword.HasData()
                  || (!SmtpUsername.HasData() && !SmtpPassword.HasData()));
         }
      }
   }
}
