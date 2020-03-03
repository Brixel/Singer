using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Singer.Helpers.Extensions;
using Singer.Models;
using Singer.Models.Users;
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
               Id = Guid.NewGuid(),
               User = new User(){LastName = "Joske Vermeulen"},
               BirthDay = DateTime.Parse("06/07/2008", CultureInfo.InvariantCulture),
               AgeGroup = AgeGroup.Child,
               IsExtern = false,
               HasTrajectory = true
            },
            new CareUser
            {
               Id = Guid.NewGuid(),
               User = new User(){LastName = "Kim Janssens"},
               BirthDay = DateTime.Parse("08/07/2006", CultureInfo.InvariantCulture),
               AgeGroup = AgeGroup.Child,
               IsExtern = true,
               HasTrajectory = true
            },
            new CareUser
            {
               Id = Guid.NewGuid(),
               User = new User(){LastName = "Benjammin Vermeulen"},
               BirthDay = DateTime.Parse("06/08/2010", CultureInfo.InvariantCulture),
               AgeGroup = AgeGroup.Youngster,
               IsExtern = false,
               HasTrajectory = true
            },
         };

         var filter = new StringFilter<CareUser>
         {
            FilterString = "true",
            PropertyList = new PropertyList<CareUser> { "IsExtern" }
         };

         list.AsQueryable().Filter(filter)
            .Should().HaveCount(1);
      }
   }
}
