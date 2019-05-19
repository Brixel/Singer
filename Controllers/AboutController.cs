using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Singer.Models;
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
      public AboutModel Index()
      {
         return new AboutModel
         {
            ApiVersion = _apiVersion
         };
      }
   }
}
