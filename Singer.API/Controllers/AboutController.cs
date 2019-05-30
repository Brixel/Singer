using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   [Authorize]
   public class AboutController : Controller
   {
      private readonly string _apiVersion;
      public AboutController()
      {
         Assembly assembly = Assembly.Load("Singer");
         var gitVersionInformationType = assembly.GetType("GitVersionInformation");
         var fullSemVerField = gitVersionInformationType.GetField("FullSemVer");
         _apiVersion = fullSemVerField.GetValue(null).ToString();
      }

      [HttpGet("")]
      public AboutDTO GetAboutVersion()
      {
         return new AboutDTO
         {
            ApiVersion = _apiVersion
         };
      }
   }
}
