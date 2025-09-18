using ScanFlowAWS.Domain.Security;

namespace ScanFlowAWS.Infrastructure.Security
{
    internal class BCryptNet : IPasswordEncripter
    {

        public string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool isValid(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
