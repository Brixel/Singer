using FluentAssertions;
using NUnit.Framework;
using Singer.Helpers.Attributes;
using Singer.Helpers.Extensions;
using Singer.Resources;

namespace Tests.HelperTests
{
   [TestFixture]
   public class CsvExtensions
   {
      public class TestModel
      {
         public int Age { get; set; }
         [CsvProperty(PropertyNameResourceType = typeof(DisplayNames), PropertyNmeResourceName = "FirstName")]
         public string Name { get; set; }
         [CsvIgnore]
         public double Weight { get; set; }
         [CsvProperty("DidNotKnowAName")]
         public double Length { get; set; }
         public string Hobby { get; set; }
      }

      [Test]
      public void SerializeCsvOne()
      {
         var model = new[]
         {
            new TestModel
            {
               Age = 21,
               Name = "Johnny",
               Weight = 72,
               Hobby = "Football",
               Length = 1.86,
            }
         };

         var csv = model.SerializeCsv(includeHeaders: false);

         csv.Should().Be("21;Johnny;1,86;Football;\r\n");
      }

      [Test]
      public void SerializeCsvMultipleWithHeader()
      {
         var model = new[]
         {
            new TestModel
            {
               Age = 21,
               Name = "Johnny",
               Weight = 72,
               Hobby = "Football",
               Length = 1.86,
            },
            new TestModel
            {
               Age = 51,
               Name = "Tom",
               Weight = 90,
               Hobby = "Piano",
               Length = 1.76,
            },
         };

         var csv = model.SerializeCsv(includeHeaders: true);

         csv.Should().Be(
            "Age;Voornaam;DidNotKnowAName;Hobby;\r\n" +
            "21;Johnny;1,86;Football;\r\n" +
            "51;Tom;1,76;Piano;\r\n");
      }
   }
}
