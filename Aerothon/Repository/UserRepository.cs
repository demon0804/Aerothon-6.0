using Aerothon.Models.Entities;
using Aerothon.Repository.Interfaces;

namespace Aerothon.Repository
{
    /// <summary>
    /// User Repository
    /// </summary>
    /// <seealso cref="IUserRepository" />
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The users
        /// </summary>
        private readonly List<User> _users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    UserName = "Harsh.Bh",
                    FirstName = "Harsh",
                    LastName = "Bharadwaj",
                    Password = "c29hUtlK9g2QEX3kXhNuAq90nU/0aL1J+TFeHyYfLXsKtfNNThNnsEm2/muQhV7u7Jm0ugEuTFrfTCOdUkGcxg==",
                    Email = "harsh.b@abc.com"
                }
            };

        /// <summary>
        /// Prevents a default instance of the <see cref="UserRepository"/> class from being created.
        /// </summary>
        public UserRepository()
        {
        }

        /// <summary>
        /// Values the tuple.
        /// </summary>
        /// <typeparam name="String">The type of the tring.</typeparam>
        /// <returns>User</returns>
        public User GetUser(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }

            return _users.Where(a => a.UserName == username).FirstOrDefault();
        }
    }
}
