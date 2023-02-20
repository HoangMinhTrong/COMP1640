using Microsoft.AspNetCore.Http;
using Utilities.Identity.DTOs;
using Utilities.Identity.Interfaces;

namespace Utilities.Identity
{
    public static class HttpContextExtenstions
    {
        public static string GetClaimValue(HttpContext context, string claimName)
        {
            if (context?.User?.Identity?.IsAuthenticated == true)
            {
                return context.User.Claims.FirstOrDefault(_ => _.Type == claimName)?.Value;
            }

            return default;
        }

        public static int UserId(this HttpContext context)
        {
            int.TryParse(GetClaimValue(context, AppClaimType.UserId), out var userId);

            return userId;
        }

        public static string UserName(this HttpContext context)
        {
            return GetClaimValue(context, AppClaimType.UserName);
        }

        public static string UserEmail(this HttpContext context)
        {
            return GetClaimValue(context, AppClaimType.UserEmail);
        }

        public static int RoleId(this HttpContext context)
        {
            int.TryParse(GetClaimValue(context, AppClaimType.RoleId), out var roleId);

            return roleId;
        }

        public static int TenantId(this HttpContext context)
        {
            int.TryParse(GetClaimValue(context, AppClaimType.TenantId), out var tenantId);
            return tenantId;
        }

        public static ICurrentUserInfo CurrentUser(this HttpContext context)
        {
            return new CurrentUserInfo(context.UserId()
                , context.UserName()
                , context.UserEmail()
                , context.RoleId()
                , context.TenantId());
        }
    }
}
