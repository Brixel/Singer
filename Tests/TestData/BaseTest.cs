using System;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using Singer.Data;

namespace Tests.TestData;

public abstract class BaseTest
{
    public ApplicationDbContext TestDataContext;

    [SetUp]
    public virtual void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(Guid.NewGuid().ToString())
           .Options;

        TestDataContext = new ApplicationDbContext(options);
    }



    [TearDown]
    public virtual void TearDown()
    {
        //TestDataContext.Dispose();
    }
}
