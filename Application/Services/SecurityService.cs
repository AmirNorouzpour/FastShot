using Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class SecurityService : ISecurityService
    {
        public string GetMd5(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var byteValue = Encoding.UTF8.GetBytes(input);
                var byteHash = sha256.ComputeHash(byteValue);
                return Convert.ToBase64String(byteHash);
            }
        }
    }
}
