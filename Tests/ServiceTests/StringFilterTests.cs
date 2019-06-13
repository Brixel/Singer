using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Services;

namespace Tests.ServiceTests
{
   [TestFixture]
   public class StringFilterTests
   {
      [Test]
      public void GetFilterExpressionExactPropertyValue()
      {
         var list = new List<CareUser>
         {
            new CareUser
            {
               Id = Guid.NewGuid().ToString(),
               Name = "Joske Vermeulen",
               BirthDay = DateTime.Parse("06/07/2008", CultureInfo.InvariantCulture),
               CaseNumber = "0123456789",
               AgeGroup = AgeGroup.Child,
               IsExtern = false,
               HasTrajectory = true,
               HasNormalDayCare = true,
               HasVacationDayCare = true,
               HasResources = true
            },
            new CareUser
            {
               Id = Guid.NewGuid().ToString(),
               Name = "Kim Janssens",
               BirthDay = DateTime.Parse("08/07/2006", CultureInfo.InvariantCulture),
               CaseNumber = "9876543210",
               AgeGroup = AgeGroup.Child,
               IsExtern = true,
               HasTrajectory = true,
               HasNormalDayCare = true,
               HasVacationDayCare = true,
               HasResources = true
            },
            new CareUser
            {
               Id = Guid.NewGuid().ToString(),
               Name = "Benjamin Vermeulen",
               BirthDay = DateTime.Parse("06/08/2010", CultureInfo.InvariantCulture),
               CaseNumber = "091837465",
               AgeGroup = AgeGroup.Youngster,
               IsExtern = false,
               HasTrajectory = true,
               HasNormalDayCare = true,
               HasVacationDayCare = true,
               HasResources = false
            },
         };

         var filter = new StringFilter<CareUser>
         {
            FilterString = "Joske Vermeulen",
            PropertyList = new PropertyList<CareUser> {"Name"}
         };

         list.AsQueryable().Filter(filter)
            .Should().HaveCount(1);
      }

      [Test]
      public void GetFilterExpressionPartialPropertyValue()
      {
         var list = new List<CareUser>
         {
            new CareUser
            {
               Id = Guid.NewGuid().ToString(),
               Name = "Joske Vermeulen",
               BirthDay = DateTime.Parse("06/07/2008", CultureInfo.InvariantCulture),
               CaseNumber = "0123456789",
               AgeGroup = AgeGroup.Child,
               IsExtern = false,
               HasTrajectory = true,
               HasNormalDayCare = true,
               HasVacationDayCare = true,
               HasResources = true
            },
            new CareUser
            {
               Id = Guid.NewGuid().ToString(),
               Name = "Kim Janssens",
               BirthDay = DateTime.Parse("08/07/2006", CultureInfo.InvariantCulture),
               CaseNumber = "9876543210",
               AgeGroup = AgeGroup.Child,
               IsExtern = true,
               HasTrajectory = true,
               HasNormalDayCare = true,
               HasVacationDayCare = true,
               HasResources = true
            },
            new CareUser
            {
               Id = Guid.NewGuid().ToString(),
               Name = "Benjamin Vermeulen",
               BirthDay = DateTime.Parse("06/08/2010", CultureInfo.InvariantCulture),
               CaseNumber = "091837465",
               AgeGroup = AgeGroup.Youngster,
               IsExtern = false,
               HasTrajectory = true,
               HasNormalDayCare = true,
               HasVacationDayCare = true,
               HasResources = false
            },
         };

         var filter = new StringFilter<CareUser>
         {
            FilterString = "Vermeulen",
            PropertyList = new PropertyList<CareUser> { "Name" }
         };

         list.AsQueryable().Filter(filter)
            .Should().HaveCount(2);
      }

      [Test]
      public void GetFilterExpressionInvariantCasePropertyValue()
      {
         var list = new List<CareUser>
         {
            new CareUser
            {
               Id = Guid.NewGuid().ToString(),
               Name = "Joske Vermeulen",
               BirthDay = DateTime.Parse("06/07/2008", CultureInfo.InvariantCulture),
               CaseNumber = "0123456789",
               AgeGroup = AgeGroup.Child,
               IsExtern = false,
               HasTrajectory = true,
               HasNormalDayCare = true,
               HasVacationDayCare = true,
               HasResources = true
            },
            new CareUser
            {
               Id = Guid.NewGuid().ToString(),
               Name = "Kim Janssens",
               BirthDay = DateTime.Parse("08/07/2006", CultureInfo.InvariantCulture),
               CaseNumber = "9876543210",
               AgeGroup = AgeGroup.Child,
               IsExtern = true,
               HasTrajectory = true,
               HasNormalDayCare = true,
               HasVacationDayCare = true,
               HasResources = true
            },
            new CareUser
            {
               Id = Guid.NewGuid().ToString(),
               Name = "Benjamin Vermeulen",
               BirthDay = DateTime.Parse("06/08/2010", CultureInfo.InvariantCulture),
               CaseNumber = "091837465",
               AgeGroup = AgeGroup.Youngster,
               IsExtern = false,
               HasTrajectory = true,
               HasNormalDayCare = true,
               HasVacationDayCare = true,
               HasResources = false
            },
         };

         var filter = new StringFilter<CareUser>
         {
            FilterString = "joske vermeulen",
            PropertyList = new PropertyList<CareUser> { "Name" }
         };

         list.AsQueryable().Filter(filter)
            .Should().HaveCount(2);
      }

      [Test]
      public void GetFilterExpressionInvariantCasePartialPropertyValue()
      {
         var list = new List<CareUser>
         {
            new CareUser
            {
               Id = Guid.NewGuid().ToString(),
               Name = "Joske Vermeulen",
               BirthDay = DateTime.Parse("06/07/2008", CultureInfo.InvariantCulture),
               CaseNumber = "0123456789",
               AgeGroup = AgeGroup.Child,
               IsExtern = false,
               HasTrajectory = true,
               HasNormalDayCare = true,
               HasVacationDayCare = true,
               HasResources = true
            },
            new CareUser
            {
               Id = Guid.NewGuid().ToString(),
               Name = "Kim Janssens",
               BirthDay = DateTime.Parse("08/07/2006", CultureInfo.InvariantCulture),
               CaseNumber = "9876543210",
               AgeGroup = AgeGroup.Child,
               IsExtern = true,
               HasTrajectory = true,
               HasNormalDayCare = true,
               HasVacationDayCare = true,
               HasResources = true
            },
            new CareUser
            {
               Id = Guid.NewGuid().ToString(),
               Name = "Benjamin Vermeulen",
               BirthDay = DateTime.Parse("06/08/2010", CultureInfo.InvariantCulture),
               CaseNumber = "091837465",
               AgeGroup = AgeGroup.Youngster,
               IsExtern = false,
               HasTrajectory = true,
               HasNormalDayCare = true,
               HasVacationDayCare = true,
               HasResources = false
            },
         };

         var filter = new StringFilter<CareUser>
         {
            FilterString = "vermeulen",
            PropertyList = new PropertyList<CareUser> { "Name" }
         };

         list.AsQueryable().Filter(filter)
            .Should().HaveCount(2);
      }
   }
}
