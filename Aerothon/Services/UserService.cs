using Aerothon.Helper.Interfaces;
using Aerothon.Models.Entities;
using Aerothon.Models.Request;
using Aerothon.Repository.Interfaces;
using Aerothon.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Aerothon.Services
{
    /// <summary>
    /// UserService
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// The password helper
        /// </summary>
        private readonly IPasswordHelper _passwordHelper;

        public UserService(
            IUserRepository userRepository,
            IPasswordHelper passwordHelper)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
        }
        /// <summary>
        /// Adds the specified user request.
        /// </summary>
        /// <param name="userRequest">The user request.</param>
        /// <returns>User Id</returns>
        public int Add(UserRequest userRequest)
        {
            if (userRequest == null)
            {
                throw new ArgumentNullException(nameof(userRequest));
            }

            ValidateRequest(userRequest);
            if (_userRepository.Get(userRequest.UserName) != null)
            {
                throw new Exception($"User with user name {userRequest.UserName} already exists");
            }

            var user = new User()
            {
                UserName = userRequest.UserName,
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Email = userRequest.Email,
                Password = _passwordHelper.HashPassword(userRequest.Password)
            };
            var id = _userRepository.Add(user);
            return id;
        }

        /// <summary>
        /// Validates the request.
        /// </summary>
        /// <param name="userRequest">The user request.</param>
        private void ValidateRequest(UserRequest userRequest)
        {
            if(string.IsNullOrEmpty(userRequest.UserName))
            {
                throw new Exception("Name cannot be empty");
            }

            if (string.IsNullOrEmpty(userRequest.FirstName))
            {
                throw new Exception("First name cannot be empty");
            }

            if (string.IsNullOrEmpty(userRequest.LastName))
            {
                throw new Exception("Last name cannot be empty");
            }

            if (string.IsNullOrEmpty(userRequest.Password))
            {
                throw new Exception("Password cannot be empty");
            }

            if (string.IsNullOrEmpty(userRequest.Email))
            {
                throw new Exception("Email cannot be empty");
            }
        }
    }
}
