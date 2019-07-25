using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Singer.DTOs;
using Singer.Models;
using Singer.Models.Users;
using Singer.Services;
using Tests.TestData;

namespace Tests.ServiceTests
{
   [TestFixture]
   class UserServiceTests : BaseTest
   {
      [Test]
      public async Task GetAllUsers()
      {
         var users = new List<CareUser>()
         {
            new CareUser()
            {
               User = new User()
               {
                  Email = "email@test.com",
                  LastName = "user",
                  UserName = "user"
               },
               AgeGroup = AgeGroup.Child,
               BirthDay = DateTime.UtcNow.AddYears(-4),
               CaseNumber = "1234",
               HasNormalDayCare = true,
               HasResources = true,
               HasTrajectory = false,
               HasVacationDayCare = false,
               IsExtern = true
            }
         };
         TestDataContext.CareUsers.AddRange(users);
         TestDataContext.SaveChanges();

         var service = new CareUserService(TestDataContext, null, null);
         var result = await service.GetUsersAsync<CareUser>("CaseNumber", "asc", "", 1, 10);
         result.Items.Should().HaveCount(1);
      }

      [Test]
      public async Task GetAllUsers_ApplyFilter()
      {
         var users = new List<CareUser>()
         {
            new CareUser()
            {
               User = new User()
               {
                  Email = "email@test.com",
                  FirstName = "firstName",
                  LastName = "user",
                  UserName = "user"
               },
               AgeGroup = AgeGroup.Child,
               BirthDay = DateTime.UtcNow.AddYears(-4),
               CaseNumber = "1234",
               HasNormalDayCare = true,
               HasResources = true,
               HasTrajectory = false,
               HasVacationDayCare = false,
               IsExtern = true
            },
            new CareUser()
            {
               User = new User()
               {
                  FirstName = "firstName",
                  Email = "email@test.com",
                  LastName = "user3",
                  UserName = "user3"
               },
               AgeGroup = AgeGroup.Child,
               BirthDay = DateTime.UtcNow.AddYears(-5),
               CaseNumber = "12345",
               HasNormalDayCare = true,
               HasResources = true,
               HasTrajectory = false,
               HasVacationDayCare = false,
               IsExtern = true
            },
            new CareUser()
            {
               User = new User()
               {
                  FirstName = "firstName",
                  Email = "email@test.com",
                  LastName = "user2",
                  UserName = "user2"
               },
               AgeGroup = AgeGroup.Child,
               BirthDay = DateTime.UtcNow.AddYears(-4),
               CaseNumber = "44",
               HasNormalDayCare = true,
               HasResources = true,
               HasTrajectory = false,
               HasVacationDayCare = false,
               IsExtern = true
            }
         };
         TestDataContext.CareUsers.AddRange(users);
         TestDataContext.SaveChanges();

         var service = new CareUserService(TestDataContext, null, null);
         var result = await service.GetUsersAsync<CareUser>("CaseNumber", "asc", "44", 1, 10);
         result.Items.Should().HaveCount(1);
         result.Items[0].LastName.Should().Be("user2");
      }

      [Test]
      public void GetUsersAsync_PageIndex0_Throws()
      {
         var service = new CareUserService(TestDataContext, null, null);

         Func<Task> f = async () =>
         {
            await service.GetUsersAsync<CareUser>("CaseNumber", "asc", "", 0, 10);
         };

         f.Should()
            .Throw<ArgumentException>()
            .WithMessage("pageIndex should be positive");

      }

      [Test]
      public void GetUsersAsync_PageSize0_Throws()
      {
         var service = new CareUserService(TestDataContext, null, null);

         Func<Task> f = async () =>
         {
            await service.GetUsersAsync<CareUser>("CaseNumber", "asc", "", 1, 0);
         };

         f.Should()
            .Throw<ArgumentException>()
            .WithMessage("pageSize should be positive");

      }
   }
}
