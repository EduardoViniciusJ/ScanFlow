namespace ScanFlowAWS.Domain.Entities
{
    public class Token
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string TokenJWT {  get; set; } = string.Empty;
        public DateTime Expiration { get; private set; }
        public bool Revoke { get; set; }    
        public string Type { get; set; }  = string.Empty ;
        private Token() { }

        public Token(Guid userId, string token, DateTime expiration, string type)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            TokenJWT = token;
            Expiration = expiration;
            Revoke = false;
            Type = type;    
        }
        public void Rekove()
        {
            Revoke = true;  
        }

    }
}
