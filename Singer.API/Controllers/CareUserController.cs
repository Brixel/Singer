using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singer.DTOs.Users;
using Singer.DTOs;
using Singer.Models.Users;
using Singer.Services;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   //[Authorize()]
   public class CareUserController : DataControllerBase<CareUser, CareUserDTO, CreateCareUserDTO, UpdateCareUserDTO>
   {
      private readonly CareUserService careUserService;
      public CareUserController(CareUserService databaseService) : base(databaseService)
      {
         careUserService = databaseService;
      }
   }
}
