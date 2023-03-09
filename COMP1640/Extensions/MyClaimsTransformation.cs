using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Utilities.Identity.DTOs;

namespace COMP1640.Extentions
{
    public class MyClaimsTransformation : IClaimsTransformation
    {
        private readonly IUserRepository _userRepo;
        private readonly SignInManager<User> _signInManager;

        public MyClaimsTransformation(IUserRepository userRepo
            , SignInManager<User> signInManager)
        {
            _userRepo = userRepo;
            _signInManager = signInManager;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity?.IsAuthenticated == false)
                return principal;

            var user = await _userRepo.FindByEmailAsync(principal.Identity.Name);
            if (user == null || user.LockoutEnabled)
            {
                await _signInManager.SignOutAsync();
                return principal;
            }

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
                 new Claim(AppClaimType.IsActivated, user.EmailConfirmed.ToString())
            };

            claimsIdentity.AddClaims(claims);
            principal.AddIdentity(claimsIdentity);
            return principal;
        }
    }
}
