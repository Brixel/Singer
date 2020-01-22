using System;
using System.Linq;
using Singer.DTOs.Users;
using Singer.DummyDataSeeder.Data.Bases;
using Singer.Models;

namespace Singer.DummyDataSeeder.Data
{
    internal sealed class CareUsers : DataContainer<CareUserDTO, CreateCareUserDTO>
    {
        private IDtoStorer<CareUserDTO, CreateCareUserDTO>[] _data;

        private readonly DataContainer<LegalGuardianUserDTO, CreateLegalGuardianUserDTO> _legalGuardians;

        public CareUsers(DataContainer<LegalGuardianUserDTO, CreateLegalGuardianUserDTO> legalGuardians)
        {
            _legalGuardians = legalGuardians;
        }

        public override IDtoStorer<CareUserDTO, CreateCareUserDTO>[] Data => _data ??= new IDtoStorer<CareUserDTO, CreateCareUserDTO>[]
        {
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
                {
                    AgeGroup = AgeGroup.Child,
                    BirthDay = new DateTime(2002, 07, 20),
                    CaseNumber = R.NewCaseNumber(),
                    FirstName = "Jimmy",
                    LastName = "Neutron",
                    HasNormalDayCare = R.NewBool(),
                    HasTrajectory = R.NewBool(),
                    HasVacationDayCare = R.NewBool(),
                    IsExtern = R.NewBool(),
                },
                LegalGuardians = _legalGuardians.Data.Where(x =>
                    x.CreateDto.Email == "nick.elodeon@nickelodeon.com" ||
                    x.CreateDto.Email == "hugh.neutron@nickelodeon.com" ||
                    x.CreateDto.Email == "judy.neutron@nickelodeon.com"
                )
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x => x.CreateDto.Email == "post@sintgerardus.be")
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x => x.CreateDto.Email == "post@sintgerardus.be")
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
                {
                    AgeGroup = AgeGroup.Toddler,
                    BirthDay = new DateTime(1993, 09, 13),
                    CaseNumber = R.NewCaseNumber(),
                    FirstName = "Dot",
                    LastName = "Animaniac",
                    HasNormalDayCare = R.NewBool(),
                    HasTrajectory = R.NewBool(),
                    HasVacationDayCare = R.NewBool(),
                    IsExtern = R.NewBool(),
                },
                LegalGuardians = _legalGuardians.Data.Where(x => x.CreateDto.Email == "post@sintgerardus.be")
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x =>
                    x.CreateDto.Email == "nick.elodeon@nickelodeon.com" ||
                    x.CreateDto.Email == "katara@nickelodeon.com" ||
                    x.CreateDto.Email == "sokka@nickelodeon.com"
                )
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x => x.CreateDto.Email == "max.tennyson@cartoon.cn")
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x =>
                    x.CreateDto.Email == "nick.elodeon@nickelodeon.com" ||
                    x.CreateDto.Email == "jack.fenton@nickelodeon.com" ||
                    x.CreateDto.Email == "maddie.fenton@nickelodeon.com"
                )
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x => x.CreateDto.Email == "kamesennin.muten.roshi@toei.jap")
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x =>
                    x.CreateDto.Email == "nick.elodeon@nickelodeon.com" ||
                    x.CreateDto.Email == "dad@nickelodeon.com" ||
                    x.CreateDto.Email == "mom@nickelodeon.com" ||
                    x.CreateDto.Email == "cosmo@nickelodeon.com" ||
                    x.CreateDto.Email == "wanda@nickelodeon.com" ||
                    x.CreateDto.Email == "vicky@nickelodeon.com"
                )
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x =>
                    x.CreateDto.Email == "fred.flinstone@stone.age" ||
                    x.CreateDto.Email == "wilma.flinstone@stone.age"
                )
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x =>
                    x.CreateDto.Email == "fred.flinstone@stone.age" ||
                    x.CreateDto.Email == "wilma.flinstone@stone.age"
                )
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x =>
                    x.CreateDto.Email == "james.timothy.possible@nickelodeon.com" ||
                    x.CreateDto.Email == "ann.possible@nickelodeon.com"
                )
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x => x.CreateDto.Email == "marge.simpson@fox.usa")
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x => x.CreateDto.Email == "marge.simpson@fox.usa")
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x => x.CreateDto.Email == "marge.simpson@fox.usa")
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x => x.CreateDto.Email == "marge.simpson@fox.usa")
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x => x.CreateDto.Email == "herman.van_veen@nederland.nl")
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x =>
                    x.CreateDto.Email == "leonard.hofstadter@cbs.usa" ||
                    x.CreateDto.Email == "penny@cbs.usa"
                )
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x => x.CreateDto.Email == "post@sintgerardus.be")
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x =>
                    x.CreateDto.Email == "phillip.banks@fresh.prince" ||
                    x.CreateDto.Email == "vivian.banks@fresh.prince"
                )
            },
            new CareUser 
            { 
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x => x.CreateDto.Email == "teddy.bear@bbc.uk")
            },
            new CareUser 
            { 
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x => x.CreateDto.Email == "john.watson@bbc.uk")
            },
            new CareUser
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x => x.CreateDto.Email == "bieke.crucke@vrt.be")
            },
            new CareUser 
            {
                CreateDto = new CreateCareUserDTO
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
                LegalGuardians = _legalGuardians.Data.Where(x => x.CreateDto.Email == "gert.verhulst@studio100.be")
            },
        };
    }
}
