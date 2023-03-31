using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Duende.IdentityServer.Extensions;

namespace Singer.Helpers.Extensions
{
    public static class PrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var userIdString = principal.GetSubjectId();
            Guid.TryParse(userIdString, out var userId);
            return userId;
        }
    }
}
