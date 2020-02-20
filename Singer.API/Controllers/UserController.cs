using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Singer.Configuration;
using Singer.Data;
using Singer.DTOs.Users;
using Singer.Helpers.Exceptions;
using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Services.Interfaces;

namespace Singer.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class UserController : ControllerBase
   {
      private readonly IUserProfileService _userProfileService;
      private readonly ApplicationDbContext _context;
      private readonly ICareUserService _careUserService;

      public UserController(IUserProfileService userProfileService,
         ApplicationDbContext context,
         ICareUserService careUserService)
      {
         _userProfileService = userProfileService;
         _context = context;
         _careUserService = careUserService;
      }
      [HttpPut("password")]
      public async Task UpdatePassword([FromBody] UpdatePasswordDTO updatePassword)
      {
         await _userProfileService.UpdatePassword(updatePassword.UserId, updatePassword.Token, updatePassword.NewPassword);
      }

      [HttpPost("resetpassword")]
      public async Task RequestPasswordReset([FromBody] string userId)
      {
         await _userProfileService.RequestPasswordReset(userId);
      }

      [Authorize]
      [HttpGet("me")]
      public async Task<UserDescriptionDTO> GetUserInfo()
      {
         var userId = User.GetUserId();

         var userDescription = TryGetUser(userId);

         userDescription.IsAdmin = User.IsInRole(Roles.ROLE_ADMINISTRATOR);

         if (!userDescription.IsAdmin) // Means is a legal guardian, since care users cannot login
         {
            var legalGuardian =
               _context.LegalGuardianUsers.SingleOrDefault(x => x.UserId == userId);
            if (legalGuardian == null)
            {
               throw new UserNotFoundException($"No legalguardian found for userId {userId}");
            }
            var relatedCareUsers =
               await _careUserService.GetCareUsersForLegalGuardian(userId);
            userDescription.CareUsers = relatedCareUsers.Select(x => new RelatedCareUserDTO()
            {
               FirstName = x.FirstName,
               LastName = x.LastName,
               AgeGroup = x.AgeGroup,
               BirthDay = x.BirthDay
            }).ToList().AsReadOnly();
            userDescription.Address = legalGuardian.Address;
            userDescription.City = legalGuardian.City;
            userDescription.Country = legalGuardian.Country;
            userDescription.PostalCode = legalGuardian.PostalCode;
         }

         return userDescription;
      }

      private UserDescriptionDTO TryGetUser(Guid userId)
      {
         var user = _context.Users.SingleOrDefault(x => x.Id == userId);
         if (user == null)
         {
            throw new UserNotFoundException($"No user found for id: {userId}");
         }

         return new UserDescriptionDTO() { Email = user.Email, FirstName = user.FirstName, LastName = user.LastName };
      }
   }

   public class RelatedCareUserDTO
   {
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public AgeGroup AgeGroup { get; set; }
      public DateTime BirthDay { get; set; }
   }

   public class UserDescriptionDTO
   {
      public string Email { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public bool IsAdmin { get; set; }
      public string Address { get; set; }
      public string City { get; set; }
      public string Country { get; set; }
      public string PostalCode { get; set; }

      public UserDescriptionDTO()
      {
         CareUsers = new List<RelatedCareUserDTO>();
      }
      public IReadOnlyList<RelatedCareUserDTO> CareUsers { get; set; }
   }
}
