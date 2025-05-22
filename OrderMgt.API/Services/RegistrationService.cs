using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore.Internal;
using System.Text;

namespace OrderMgt.API.Services
{
    public class RegistrationService: Interfaces.IRegistrationService
    {
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
