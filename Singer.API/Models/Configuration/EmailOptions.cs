namespace Singer.Data.Models.Configuration
{
   public class EmailOptions
   {
      public string SmtpHost { get; set; }
      public string SmtpUsername { get; set; }
      public string SmtpPassword { get; set; }
      public int SmtpPort { get; set; }
      public string MailFrom { get; set; }
      public bool EnableSsl { get; set; }
   }
}
