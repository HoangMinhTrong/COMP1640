using Microsoft.AspNetCore.Identity;

namespace Utilities.Identity.Interfaces
{
    public interface IAuthenticationService
    {
        Task<SignInResult> Authenticate(string userName, string password);
        Task Signout();
    }
}
