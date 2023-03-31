using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using FluentAssertions;

using Moq;

using NUnit.Framework;
using NUnit.Framework.Internal;

using Singer.Helpers.Extensions;
using Singer.Models.Users;
using Singer.Services;
using Singer.Services.Interfaces;

using Tests.TestData;

namespace Tests.ServiceTests;

[TestFixture]
public class EventRegistrationServiceTest : BaseTest
{
    private CareUser _careUser;

    [SetUp]
    public override void Setup()
    {
        base.Setup();
        _careUser = new CareUser();
        TestDataContext.CareUsers.Add(_careUser);
        TestDataContext.SaveChanges();
    }

    [Test]
    public async Task Create_care_registrations_for_one_user_and_one_day()
    {

        var sut = new EventRegistrationService(TestDataContext, It.IsAny<IMapper>(),
           It.IsAny<IActionNotificationService>());
        var startDate = new DateTime(2020, 2, 10, 8, 0, 0);
        var endDate = new DateTime(2020, 2, 10, 17, 0, 0);

        var registrationIds =
           await sut.Create(Singer.Models.RegistrationTypes.DayCare, new List<Guid> { _careUser.Id }, startDate, endDate);
        registrationIds.Count.Should().Be(1);
    }



    [Test]
    public async Task Create_care_registrations_for_one_user_and_starting_on_friday_ending_monday()
    {

        var sut = new EventRegistrationService(TestDataContext, It.IsAny<IMapper>(),
           It.IsAny<IActionNotificationService>());
        var startDate = new DateTime(2020, 2, 14, 8, 0, 0); // Friday
        var endDate = new DateTime(2020, 2, 17, 17, 0, 0); // Monday

        var registrationIds =
           await sut.Create(Singer.Models.RegistrationTypes.DayCare, new List<Guid> { _careUser.Id }, startDate, endDate);
        registrationIds.Count.Should().Be(2);

        var registrations = TestDataContext.Registrations
           .Where(x => registrationIds.Contains(x.Id))
           .OrderBy(x => x.StartDateTime)
           .ToList();
        registrations[0].CareUserId.Should().Be(_careUser.Id);
        registrations[0].StartDateTime.Should().Be(new DateTime(2020, 2, 14, 8, 0, 0));
        registrations[0].EndDateTime.Should().Be(new DateTime(2020, 2, 14, 17, 0, 0));
        registrations[1].CareUserId.Should().Be(_careUser.Id);
        registrations[1].StartDateTime.Should().Be(new DateTime(2020, 2, 17, 8, 0, 0));
        registrations[1].EndDateTime.Should().Be(new DateTime(2020, 2, 17, 17, 0, 0));
    }
}
