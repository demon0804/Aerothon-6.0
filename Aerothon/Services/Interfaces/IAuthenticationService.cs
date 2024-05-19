using Aerothon.Models.Request;
using Aerothon.Models.Response;

namespace Aerothon.Services.Interfaces
{
    /// <summary>
    /// IAuthentication Service.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Logins the specified login request.
        /// </summary>
        /// <param name="loginRequest">The login request.</param>
        /// <returns>User response</returns>
        UserResponse Login(LoginRequest loginRequest);
    }
}
