using Domain;
using Microsoft.AspNetCore.Identity;
using Utilities.Identity.Interfaces;

namespace Utilities.Identity
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<User> _signInManager;

        public AuthenticationService(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> Authenticate(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, false, lockoutOnFailure: false);
            return result;
        }

        public async Task Signout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
