using System;

using Microsoft.AspNetCore.Identity;

using Singer.Helpers;

namespace Singer.Models.Users;

public class User : IdentityUser<Guid>, IIdentifiable
{
    [PersonalData]
    public string FirstName { get; set; }
    [PersonalData]
    public string LastName { get; set; }
}
