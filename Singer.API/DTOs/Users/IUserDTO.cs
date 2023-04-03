using Singer.Helpers;

namespace Singer.DTOs.Users;

public interface IUserDTO : IIdentifiable
{
    string FirstName { get; set; }

    string LastName { get; set; }

    string Email { get; set; }
}

public interface ICreateUserDTO
{
    string FirstName { get; set; }

    string LastName { get; set; }

    string Email { get; set; }
}

public interface IUpdateUserDTO
{
    string FirstName { get; set; }

    string LastName { get; set; }

    string Email { get; set; }
}
