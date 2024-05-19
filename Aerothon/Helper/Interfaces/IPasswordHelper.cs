namespace Aerothon.Helper.Interfaces
{
    /// <summary>
    /// IPasswordHelper
    /// </summary>
    public interface IPasswordHelper
    {
        /// <summary>
        /// Hashes the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>Hash password</returns>
        string HashPassword(string password);

        /// <summary>
        /// Verifies the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <returns></returns>
        bool VerifyPassword(string password, string hashedPassword);
    }
}
