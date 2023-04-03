using System;
using System.Security.Claims;

using Duende.IdentityServer.Extensions;

namespace Singer.Helpers.Extensions;

public static class PrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        var userIdString = principal.GetSubjectId();
        var userId = Guid.Parse(userIdString);
        return userId;
    }
}
