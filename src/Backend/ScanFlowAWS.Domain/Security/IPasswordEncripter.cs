namespace ScanFlowAWS.Domain.Security
{
    public interface IPasswordEncripter
    {
        public string Encrypt(string password);
        public bool isValid(string password, string passwordHash);
    }
}
