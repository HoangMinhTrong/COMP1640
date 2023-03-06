using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Utilities.Identity.DTOs;

namespace Utilities.ValidataionAttributes
{
    public class COMP1640Authorize : AuthorizeAttribute, IAuthorizationFilter
    {
        private RoleTypeEnum[] RoleTypes;
        public COMP1640Authorize(params  RoleTypeEnum[] roleTypes)
        {
            RoleTypes = roleTypes;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                return;
            }

            int.TryParse(user.Claims.FirstOrDefault(_ => _.Type == AppClaimType.RoleId)?.Value, out var roleId);
            var isAuthorized = RoleTypes.Contains((RoleTypeEnum)roleId);
            if (!isAuthorized)
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                return;
            }
        }
    }

}
