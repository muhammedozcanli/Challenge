using System.Security.Cryptography;
using System.Text;

namespace Challenge.Common.Security
{
    public static class HashingHelper
    {
        /// <summary>
        /// Generates a SHA-256 hash of the specified password and returns it as a Base64-encoded string.
        /// </summary>
        /// <param name="password">The plain text password to be hashed.</param>
        /// <returns>A Base64-encoded string representing the SHA-256 hash of the password.</returns>
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

    }
} 