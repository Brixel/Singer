using System;
using FluentAssertions;
using NUnit.Framework;
using Singer.DTOs;
using Singer.Services;

namespace Tests.ServiceTests
{
   [TestFixture]
   public class SorterTests
   {
      [Test]
      public void AddProperty()
      {
         var sorter = new Sorter<UserDTO>();

         sorter.Add(nameof(UserDTO.Id));

         sorter.Should().Contain(nameof(UserDTO.Id));

         sorter.Invoking(x => x.Add(null))
            .Should()
            .Throw<ArgumentNullException>("The given property is null");
      }
   }
}
