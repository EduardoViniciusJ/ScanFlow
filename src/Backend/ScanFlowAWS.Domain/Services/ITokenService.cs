using ScanFlowAWS.Domain.Entities;

namespace ScanFlowAWS.Domain.Services
{
    public interface ITokenService
    {
        Token CreateToken(User user);
        Token RefreshToken(User user);
    }
}
