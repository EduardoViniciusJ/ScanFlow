namespace ScanFlowAWS.Domain.Entities
{
    public class User
    {

        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string PasswordHash = string.Empty;

        private User() { }  

        public User(string email, string passwordHash)
        {
            Id = Guid.NewGuid();    
            Email = email;  
            PasswordHash = passwordHash;
        }

    }
}
