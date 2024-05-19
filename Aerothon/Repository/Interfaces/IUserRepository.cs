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
    }
}
