using Amazon.Rekognition.Model;
using ScanFlowAWS.Domain.Entities;
using ScanFlowAWS.Domain.Repositories.Token;
using ScanFlowAWS.Infrastructure.DataAcess.Context;

namespace ScanFlowAWS.Infrastructure.DataAcess.Repositories
{
    public class TokenRepository : ITokenWriteOnlyRepository
    {
        private readonly ScanFlowAWSDbContext _context;

        public TokenRepository(ScanFlowAWSDbContext context)
        {
            _context = context; 
        }
        public async Task AddAsync(Token token)
        {
            await _context.Tokens.AddAsync(token);
        }
    }
}
