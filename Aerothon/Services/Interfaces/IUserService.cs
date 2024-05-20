using Aerothon.Models.Request;

namespace Aerothon.Services.Interfaces
{
    /// <summary>
    /// IUserService
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Adds the specified user request.
        /// </summary>
        /// <param name="userRequest">The user request.</param>
        /// <returns>User Id</returns>
        int Add(UserRequest userRequest);
    }
}
