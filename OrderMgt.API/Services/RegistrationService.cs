using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore.Internal;
using System.Text;

namespace OrderMgt.API.Services
{
    /// <summary>
    /// Provides functionality for user registration, including password creation and hashing.
    /// </summary>
    /// <remarks>This service is responsible for generating secure hashed passwords using a predefined salt
    /// and  the PBKDF2 key derivation function with HMACSHA256. It is intended to be used as part of a  registration or
    /// authentication workflow.</remarks>
    public class RegistrationService: Interfaces.IRegistrationService
    {
        /// <summary>
        /// Creates a hashed representation of the specified password using PBKDF2 with HMACSHA256.
        /// </summary>
        /// <remarks>This method uses a fixed salt and performs 100,000 iterations of the PBKDF2 algorithm
        /// with HMACSHA256 to derive a 256-bit subkey. The resulting hash is encoded as a base64 string.</remarks>
        /// <param name="password">The plain-text password to hash. Cannot be null.</param>
        /// <returns>A base64-encoded string representing the hashed password.</returns>
        public string CreatePassword(string password)
        {
            byte[] salt = Encoding.ASCII.GetBytes("FB859366-68D2-48C0-BA78-484E4A167E8D"); 

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
