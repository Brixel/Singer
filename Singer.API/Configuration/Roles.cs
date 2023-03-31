using System.Collections.Generic;

namespace Singer.Configuration;

public static class Roles
{
    public const string ROLE_ADMINISTRATOR = "Administrator";
    public const string ROLE_SOCIALSERVICES = "SocialServices";
    public const string ROLE_LEGALGUARDIANUSER = "LegalGuardian";
    public const string ROLE_CAREUSER = "CareUser";

    public static List<string> CareUsers = new() { "user1", "user2", "user3" };

    public static List<string> ROLES = new()
    {
        ROLE_ADMINISTRATOR,
        ROLE_SOCIALSERVICES,
        ROLE_LEGALGUARDIANUSER,
        ROLE_CAREUSER
    };
}
