using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Utilities.Identity.DTOs;

namespace COMP1640.Extentions
{
    public class MyClaimsTransformation : IClaimsTransformation
    {
        private readonly IUserRepository _userRepo;

        public MyClaimsTransformation(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity?.IsAuthenticated == false)
                return principal;

            var user = await _userRepo.FindByEmailAsync(principal.Identity.Name);
            if (user == null)
                return principal;

            // Create a claims identity for the user
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            var claims = new List<Claim>
            {
                new Claim(AppClaimType.UserId, user.Id.ToString()),
                new Claim(AppClaimType.UserName, user.UserName),
                new Claim(AppClaimType.UserEmail, user.Email),
                new Claim(AppClaimType.RoleId, user.RoleUsers
                    .Select(_ => _.RoleId)
                    .FirstOrDefault()
                    .ToString()),
                new Claim(AppClaimType.TenantId, user.TenantUsers
                    .Select(_ => _.TenantId)
                    .FirstOrDefault()
                    .ToString()),
                new Claim(AppClaimType.DepartmentId, user.UserDepartments
                    .Select(_ => _.DepartmentId)
                    .FirstOrDefault()
                    .ToString()),
            };

            claimsIdentity.AddClaims(claims);
            principal.AddIdentity(claimsIdentity);
            return principal;
        }
    }
}
