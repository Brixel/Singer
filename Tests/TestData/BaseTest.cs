using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Singer.Data;

namespace Tests.TestData
{
   public abstract class BaseTest
   {
      public ApplicationDbContext TestDataContext;

      [SetUp]
      public void Setup()
      {
         var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

         TestDataContext = new ApplicationDbContext(options);
      }



      [TearDown]
      public virtual void TearDown()
      {
         TestDataContext.Dispose();
      }
   }


}
