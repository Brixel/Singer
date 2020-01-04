namespace Singer.Data.Models.Configuration
{
   public class ApplicationConfig
   {
      public string CertFileName { get; set; }
      public string CertPass { get; set; }
      public string CertThumbprint { get; set; }
      public string InitialAdminUserPassword { get; set; }
      public string Authority { get; set; }

      public string FrontendURL { get; set; }
   }
}
