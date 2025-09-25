using Amazon.Rekognition.Model;
using Microsoft.EntityFrameworkCore;
using ScanFlowAWS.Domain.Entities;
using ScanFlowAWS.Domain.Repositories.Token;
using ScanFlowAWS.Infrastructure.DataAcess.Context;

namespace ScanFlowAWS.Infrastructure.DataAcess.Repositories
{
    public class TokenRepository : ITokenWriteOnlyRepository, ITokenReadOnlyRepository
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

        public void Delete(Token token)
        {
            _context.Tokens.Remove(token);
        }

        public async Task<Token> GetByTokenAsync(string token)
        {
            return await _context.Tokens
               .AsNoTracking()
               .FirstAsync(t => t.TokenJWT == token);
        }
    }
}
