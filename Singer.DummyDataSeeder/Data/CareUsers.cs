using System;

using Singer.DTOs.Users;
using Singer.Models;

namespace Singer.DummyDataSeeder.Data
{
    internal sealed class CareUsers : DataContainer<CreateCareUserDTO>
    {
        private CreateCareUserDTO[] _data;

        public override CreateCareUserDTO[] Data => _data ??= new[]
        {
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Child,
                BirthDay = new DateTime(2002, 07, 20),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "jimmy",
                LastName = "neutron",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Toddler,
                BirthDay = new DateTime(1993, 09, 13),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Yakko",
                LastName = "Animaniac",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Toddler,
                BirthDay = new DateTime(1993, 09, 13),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Wakko",
                LastName = "Animaniac",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Toddler,
                BirthDay = new DateTime(1993, 09, 13),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "dot",
                LastName = "animaniac",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Youngster,
                BirthDay = new DateTime(2005, 02, 21),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Aang",
                LastName = "Avatar",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Youngster,
                BirthDay = new DateTime(1993, 09, 13),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Ben",
                LastName = "Tennyson",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Youngster,
                BirthDay = new DateTime(2004, 04, 3),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Danny",
                LastName = "Fenton",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Adult,
                BirthDay = new DateTime(1989, 04, 19),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Goku",
                LastName = "Saiyan",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Child,
                BirthDay = new DateTime(2001, 03, 30),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Timmy",
                LastName = "Turner",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Youngster,
                BirthDay = new DateTime(1960, 09, 30),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Pebbles",
                LastName = "Flinstone",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Toddler,
                BirthDay = new DateTime(1960, 09, 30),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Puss",
                LastName = "Flinstone",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Youngster,
                BirthDay = new DateTime(2007, 07, 07),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Kim",
                LastName = "Possible",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Adult,
                BirthDay = new DateTime(1989, 12, 17),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Homer",
                LastName = "Simpson",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Youngster,
                BirthDay = new DateTime(1989, 12, 17),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Bart",
                LastName = "Simpson",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Child,
                BirthDay = new DateTime(1989, 12, 17),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Lisa",
                LastName = "Simpson",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Kindergartener,
                BirthDay = new DateTime(1989, 12, 17),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Maggy",
                LastName = "Simpson",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Child,
                BirthDay = new DateTime(1989, 04, 03),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Alfred Jodocus",
                LastName = "Kwak",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Adult,
                BirthDay = new DateTime(2007, 09, 24),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Sheldon",
                LastName = "Cooper",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Adult,
                BirthDay = default(DateTime).AddMonths(11).AddDays(23),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Doctor",
                LastName = "The",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Adult,
                BirthDay = new DateTime(1990, 09, 10),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Will",
                LastName = "Smith",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Child,
                BirthDay = new DateTime(1995, 12, 15),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Bean",
                LastName = "Mr.",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Adult,
                BirthDay = new DateTime(2011, 06, 05),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Sherlock",
                LastName = "Holmes",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Adult,
                BirthDay = new DateTime(1990, 10, 06),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Mark",
                LastName = "Vertongen",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
            new CreateCareUserDTO
            {
                AgeGroup = AgeGroup.Child,
                BirthDay = new DateTime(1990, 09, 02),
                CaseNumber = R.NewCaseNumber(),
                FirstName = "Samson",
                LastName = "De hond",
                HasNormalDayCare = R.NewBool(),
                HasTrajectory = R.NewBool(),
                HasVacationDayCare = R.NewBool(),
                IsExtern = R.NewBool(),
            },
        };
    }
}