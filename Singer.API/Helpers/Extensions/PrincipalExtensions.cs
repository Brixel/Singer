using System;
using System.Security.Claims;

using Microsoft.Identity.Web;

namespace Singer.Helpers.Extensions;

public static class PrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        var userIdString = principal.GetObjectId();
        var userId = Guid.Parse(userIdString);
        return userId;
    }
}
