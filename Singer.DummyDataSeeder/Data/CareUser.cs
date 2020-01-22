using System.Collections.Generic;
using Singer.DTOs.Users;
using Singer.DummyDataSeeder.Data.Bases;

namespace Singer.DummyDataSeeder.Data
{
    internal class CareUser : DtoStorer<CareUserDTO, CreateCareUserDTO>
    {
        public IEnumerable<IDtoStorer<LegalGuardianUserDTO, CreateLegalGuardianUserDTO>> LegalGuardians { get; set; }
    }
}
