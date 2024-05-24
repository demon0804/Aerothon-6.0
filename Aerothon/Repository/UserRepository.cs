using Aerothon.Models.Entities;
using Aerothon.Repository.Interfaces;
using Dapper;
using Npgsql;

namespace Aerothon.Repository
{
    /// <summary>
    /// User Repository
    /// </summary>
    /// <seealso cref="IUserRepository" />
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string _connectionString = "Host=abul.db.elephantsql.com;Username=bvchllxv;Password=0p4MvPGG25obquUww-Sg6optVy6MUJwl;Database=bvchllxv";

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
        public User Get(string username)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var query = $@"SELECT 
                ""id"", 
                ""UserName"", 
                ""FirstName"", 
                ""LastName"", 
                ""Password"", 
                ""Email""
            FROM 
                ""User""
            WHERE 
                 ""UserName"" = @Username
            LIMIT 1
";
            return connection.QuerySingleOrDefault<User>(query, new { Username = username });
        }

        /// <summary>
        /// Adds the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public int Add(User user)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var query = $@"
                           INSERT INTO ""User"" (""UserName"", ""Password"", ""FirstName"", ""LastName"", ""Email"")
                           VALUES (@UserName, @Password, @FirstName, @LastName, @Email)
                    ";
            return connection.Execute(query, user);
        }
    }
}
