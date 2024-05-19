using Aerothon.Helper.Interfaces;
using Aerothon.Models.Request;
using Aerothon.Models.Response;
using Aerothon.Repository.Interfaces;
using Aerothon.Services.Interfaces;

namespace Aerothon.Services
{
    /// <summary>
    /// Authentication service
    /// </summary>
    /// <seealso cref="IAuthenticationService" />
    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// The password helper
        /// </summary>
        private readonly IPasswordHelper _passwordHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="passwordHelper">The password helper.</param>
        public AuthenticationService(
            IUserRepository userRepository,
            IPasswordHelper passwordHelper)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
        }

        /// <summary>
        /// Logins the specified login request.
        /// </summary>
        /// <param name="loginRequest">The login request.</param>
        /// <returns>User response</returns>
        public UserResponse Login(LoginRequest loginRequest)
        {
            if(loginRequest == null)
            {
                throw new ArgumentNullException(nameof(loginRequest));
            }

            ValidateUser(loginRequest);
            var user = _userRepository.Get(loginRequest.UserName);
            if(user == null)
            {
                throw new Exception($"User with user name {loginRequest.UserName} does not exists");
            }

            var hashPassword = _passwordHelper.HashPassword(loginRequest.Password);
            if (_passwordHelper.VerifyPassword(loginRequest.Password, hashPassword))
            {
                return new UserResponse()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName
                };
            }
            else
            {
                throw new Exception("Password does not match. Please try again");
            }
        }

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="loginRequest">The login request.</param>
        private void ValidateUser(LoginRequest loginRequest)
        {
            if(string.IsNullOrEmpty(loginRequest.UserName))
            {
                throw new Exception("Username cannot be empty");
            }

            if (string.IsNullOrEmpty(loginRequest.Password))
            {
                throw new Exception("Password cannot be empty");
            }
        }
    }
}
