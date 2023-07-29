using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MAV.Cms.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid? GetUserId(this ClaimsPrincipal user)
        {
            var value = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (value.HasValue())
            {
                return Guid.Parse(value);
            }
            return null;
        }
        public static List<string> GetUserRoles(this ClaimsPrincipal user)
        {
            List<string> roles = null;

            var claims = user.FindAll(ClaimTypes.Role).ToList();
            if (claims != null && claims.Any())
            {
                roles = new List<string>();
                for (int i = 0; i < claims.Count; i++)
                {
                    roles.Add(claims[i]?.Value);
                }
            }
            return roles;
        }
    }
}
