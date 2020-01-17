using Singer.DTOs.Users;
using Singer.DummyDataSeeder.Data.Bases;

namespace Singer.DummyDataSeeder.Data
{
    internal class LegalGuardians : DataContainer<LegalGuardianUserDTO, CreateLegalGuardianUserDTO>
    {
        private IDtoStorer<LegalGuardianUserDTO, CreateLegalGuardianUserDTO>[] _data;

        public override IDtoStorer<LegalGuardianUserDTO, CreateLegalGuardianUserDTO>[] Data => _data ??= new IDtoStorer<LegalGuardianUserDTO, CreateLegalGuardianUserDTO>[]
        {
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "231 W Olive Ave.",
                    Country = "USA",
                    PostalCode = "CA 91502",
                    FirstName = "Nick",
                    LastName = "Elodeon",
                    Email = "nick.elodeon@nickelodeon.com",
                    City = "Burbank",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "Located on one side of the street from Cindy Vortex's House.",
                    Country = "USA",
                    PostalCode = "73301",
                    FirstName = "Hugh",
                    LastName = "Neutron",
                    Email = "hugh.neutron@nickelodeon.com",
                    City = "Retroville Texas",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "Located on one side of the street from Cindy Vortex's House.",
                    Country = "USA",
                    PostalCode = "73301",
                    FirstName = "Judy",
                    LastName = "Neutron",
                    Email = "judy.neutron@nickelodeon.com",
                    City = "Retroville Texas",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "South Pole",
                    Country = "South Pole",
                    PostalCode = "00001",
                    FirstName = "Katara",
                    LastName = "Water",
                    Email = "katara@nickelodeon.com",
                    City = "The only village",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "South Pole",
                    Country = "South Pole",
                    PostalCode = "00001",
                    FirstName = "Sokka",
                    LastName = "Water",
                    Email = "sokka@nickelodeon.com",
                    City = "The only village",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "Ghosts 101",
                    Country = "USA",
                    PostalCode = "666",
                    FirstName = "Jack",
                    LastName = "Fenton",
                    Email = "jack.fenton@nickelodeon.com",
                    City = "Amity Park",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "Ghosts 101",
                    Country = "USA",
                    PostalCode = "666",
                    FirstName = "Maddie",
                    LastName = "Fenton",
                    Email = "maddie.fenton@nickelodeon.com",
                    City = "Amity Park",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "Main Beach 1",
                    Country = "5 Island on the right ",
                    PostalCode = "MB1",
                    FirstName = "Kamesennin Muten",
                    LastName = "Roshi",
                    Email = "kamesennin.muten.roshi@toei.jap",
                    City = "No city",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "Second street 56",
                    Country = "USA",
                    PostalCode = "CA6369",
                    FirstName = "Dad",
                    LastName = "Timmy's",
                    Email = "dad@nickelodeon.com",
                    City = "Dimmsdale California",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "Second street 56",
                    Country = "USA",
                    PostalCode = "CA6369",
                    FirstName = "Mom",
                    LastName = "Timmy's",
                    Email = "mom@nickelodeon.com",
                    City = "Dimmsdale California",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "Second street 56",
                    Country = "USA",
                    PostalCode = "CA6369",
                    FirstName = "Cosmo",
                    LastName = "Fairy",
                    Email = "cosmo@nickelodeon.com",
                    City = "Dimmsdale California",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "Second street 56",
                    Country = "USA",
                    PostalCode = "CA6369",
                    FirstName = "Wanda",
                    LastName = "Fairy",
                    Email = "wanda@nickelodeon.com",
                    City = "Dimmsdale California",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "Second street 28",
                    Country = "USA",
                    PostalCode = "CA6369",
                    FirstName = "Vicky",
                    LastName = "Babysitter",
                    Email = "vicky@nickelodeon.com",
                    City = "Dimmsdale California",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "345 Cave Stone Road",
                    Country = "Stone age",
                    PostalCode = "B894",
                    FirstName = "Fred",
                    LastName = "Flinstone",
                    Email = "fred.flinstone@stone.age",
                    City = "Bedrock",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "345 Cave Stone Road",
                    Country = "Stone age",
                    PostalCode = "B894",
                    FirstName = "Wilma",
                    LastName = "Flinstone",
                    Email = "fred.flinstone@stone.age",
                    City = "Bedrock",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "Main street 7",
                    Country = "USA",
                    PostalCode = "B894",
                    FirstName = "Dr. James Timothy",
                    LastName = "Possible",
                    Email = "james.timothy.possible@nickelodeon.com",
                    City = "Middleton",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "Main street 7",
                    Country = "USA",
                    PostalCode = "B894",
                    FirstName = "Dr. Ann",
                    LastName = "Possible",
                    Email = "ann.possible@nickelodeon.com",
                    City = "Middleton",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "742 Evergreen Terrace",
                    Country = "USA",
                    PostalCode = "58008",
                    FirstName = "Marge",
                    LastName = "Simpson",
                    Email = "marge.simpson@fox.usa",
                    City = "Springfield",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "Landgoed de Paltz 1",
                    Country = "Nederland",
                    PostalCode = "3768 MZ",
                    FirstName = "Herman",
                    LastName = "van Veen",
                    Email = "herman.van_veen@nederland.nl",
                    City = "Soest",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "2311 North Los Robles Avenue 4A",
                    Country = "USA",
                    PostalCode = "91001 MZ",
                    FirstName = "Leonard",
                    LastName = "Hofstadter",
                    Email = "leonard.hofstadter@cbs.usa",
                    City = "Pasadena California",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "2311 North Los Robles Avenue 4A",
                    Country = "USA",
                    PostalCode = "91001 MZ",
                    FirstName = "Penny",
                    LastName = "Ynnep",
                    Email = "penny@cbs.usa",
                    City = "Pasadena California",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "251 North Bristol Avenue",
                    Country = "USA",
                    PostalCode = "CA 90049",
                    FirstName = "Phillip",
                    LastName = "Banks",
                    Email = "phillip.banks@fresh.prince",
                    City = "Los Angeles, CA",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "251 North Bristol Avenue",
                    Country = "USA",
                    PostalCode = "CA 90049",
                    FirstName = "Vivian",
                    LastName = "Banks",
                    Email = "vivian.banks@fresh.prince",
                    City = "Los Angeles, CA",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "12 Arbour Road",
                    Country = "UK",
                    PostalCode = "N5",
                    FirstName = "Teddy",
                    LastName = "Bear",
                    Email = "teddy.bear@bbc.uk",
                    City = "Highbury",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "221b Baker St",
                    Country = "UK",
                    PostalCode = "NW1 6XE",
                    FirstName = "DR. John",
                    LastName = "Watson",
                    Email = "john.watson@bbc.uk",
                    City = "London",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "Ranstsesteenweg 88",
                    Country = "België",
                    PostalCode = "2520",
                    FirstName = "Bieke",
                    LastName = "Crucke",
                    Email = "bieke.crucke@bbc.uk",
                    City = "Antwerpen",
                },
            },
            new LegalGuardian
            {
                CreateDto = new CreateLegalGuardianUserDTO
                {
                    Address = "Leon Dumortierstraat 67",
                    Country = "België",
                    PostalCode = "2540",
                    FirstName = "Gert",
                    LastName = "Verhulst",
                    Email = "gert.verhulst@studio100.be",
                    City = "Hove",
                },
            },
        };
    }
}