using Singer.DTOs.Users;
using Singer.DummyDataSeeder.Data.Bases;

namespace Singer.DummyDataSeeder.Data
{
    internal class Admins : DataContainer<AdminUserDTO, CreateAdminUserDTO>
    {
        private IDtoStorer<AdminUserDTO, CreateAdminUserDTO>[] _data;

        public override IDtoStorer<AdminUserDTO, CreateAdminUserDTO>[] Data => _data ??= new IDtoStorer<AdminUserDTO, CreateAdminUserDTO>[]
        {
            new Admin
            {
                CreateDto = new CreateAdminUserDTO
                {
                    Email = "alex.goris@brixel.be",
                    FirstName = "Alex",
                    LastName = "Goris",
                }
            },
            new Admin
            {
                CreateDto = new CreateAdminUserDTO
                {
                    Email = "berend.wouters@brixel.be",
                    FirstName = "Berend",
                    LastName = "Wouters"
                }
            },
            new Admin
            {
                CreateDto = new CreateAdminUserDTO
                {
                    Email = "joren.thijs@brixel.be",
                    FirstName = "Joren",
                    LastName = "Thijs"
                }
            },
            new Admin
            {
                CreateDto = new CreateAdminUserDTO
                {
                    Email = "wim.van.laer@brixel.be",
                    FirstName = "Wim",
                    LastName = "Van Laer",
                },
            },
        };
    }
}
