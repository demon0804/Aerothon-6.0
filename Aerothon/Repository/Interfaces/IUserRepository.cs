using Aerothon.Models.Entities;

namespace Aerothon.Repository.Interfaces
{
    /// <summary>
    /// IUserRepository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Values the tuple.
        /// </summary>
        /// <typeparam name="String">The type of the tring.</typeparam>
        /// <returns>User</returns>
        public User Get(string username);

        /// <summary>
        /// Adds the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public int Add(User user);
    }
}
