using System.Collections.Generic;
using Singer.DTOs.Users;
using Singer.DummyDataSeeder.Data.Bases;

namespace Singer.DummyDataSeeder.Data
{
    internal class LegalGuardian : DtoStorer<LegalGuardianUserDTO, CreateLegalGuardianUserDTO>
    {
        public IEnumerable<IDtoStorer<CareUserDTO, CreateCareUserDTO>> CareUsers { get; set; }
    }
}
