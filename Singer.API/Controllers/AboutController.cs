using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   public class AboutController : Controller
   {
      private string _apiVersion;
      public AboutController()
      {
         Assembly assembly = Assembly.Load("Singer");
         var gitVersionInformationType = assembly.GetType("GitVersionInformation");
         var fullSemVerField = gitVersionInformationType.GetField("FullSemVer");
         _apiVersion = fullSemVerField.GetValue(null).ToString();
      }
      public AboutDTO Index()
      {
         return new AboutDTO
         {
            ApiVersion = _apiVersion
         };
      }
   }
}
