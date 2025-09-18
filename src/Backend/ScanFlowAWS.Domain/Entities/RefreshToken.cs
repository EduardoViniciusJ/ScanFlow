namespace ScanFlowAWS.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token {  get; set; } = string.Empty;
        public DateTime Expiration { get; private set; }
        public bool Revoke { get; set; }    
        private RefreshToken() { }

        public RefreshToken(Guid userId, string token, DateTime expiration)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Token = token;
            Expiration = expiration;
            Revoke = false;
        }

        public void Rekove()
        {
            Revoke = true;  
        }

    }
}
