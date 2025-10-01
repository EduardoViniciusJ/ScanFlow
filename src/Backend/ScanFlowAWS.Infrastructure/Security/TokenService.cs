using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ScanFlowAWS.Domain.Entities;
using ScanFlowAWS.Domain.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ScanFlowAWS.Infrastructure.Security
{
    /// <summary>
    /// Serviço responsável por criar tokens JWT de acesso e Refresh Tokens para autenticação.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Construtor do serviço <see cref="TokenService"/>.
        /// </summary>
        /// <param name="configuration">Serviço de configuração para acessar as chaves e parâmetros do JWT.</param>
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Cria um token de acesso (JWT) para o usuário informado.
        /// </summary>
        /// <param name="user">Usuário para quem o token será gerado.</param>
        /// <returns>Objeto <see cref="Token"/> contendo o JWT, tempo de expiração e tipo "Access".</returns>
        public Token CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var secret = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(3);

            var token = new JwtSecurityToken(
               issuer: issuer,
               audience: audience,
               claims: claims,
               expires: expiration,
               signingCredentials: creds
           );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new Token(user.Id, tokenString, expiration, "Access");
        }

        /// <summary>
        /// Cria um Refresh Token aleatório para o usuário informado.
        /// </summary>
        /// <param name="user">Usuário para quem o Refresh Token será gerado.</param>
        /// <returns>Objeto <see cref="Token"/> contendo o token aleatório, tempo de expiração e tipo "Refresh".</returns>
        public Token RefreshToken(User user)
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            var tokenString = Convert.ToBase64String(randomBytes);

            var expiration = DateTime.UtcNow.AddMinutes(5);

            return new Token(user.Id, tokenString, expiration, "Refresh");
        }
    }
}

